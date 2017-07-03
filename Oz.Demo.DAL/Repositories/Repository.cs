using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Configuration;
using Oz.Demo.DAL.Model;
using System.Data.Entity;

namespace Oz.Demo.DAL.Repositories
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }


    /// <summary>
    /// Абстрактное описание репозитория с подключением MSSQL через контекст Entity Framework 
    /// </summary>
    /// <typeparam name="T">Тип данных репозитория</typeparam>
    /// <typeparam name="M">Типа данных идентификатора</typeparam>
    public abstract class Repository<T> : IRepository<T>
        where T : Record
    {       
        public string ActiveUser { get; set; }
        protected OzDemoDbContext DbContext { get; private set; }

        public Repository(string activeUser)
        {
            ActiveUser = activeUser;
            DbContext = new OzDemoDbContext();
            int commandTimeout=180;
            //int.TryParse(ConfigurationManager.AppSettings["DbCommandTimeout"], out commandTimeout);
            this.DbContext.Database.CommandTimeout = commandTimeout;
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> action)
        {
            return DbContext.Set<T>().Where(action);
        }

        public virtual T Get(int id)
        {
            return DbContext.Set<T>().FirstOrDefault(o => o.ID==id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(o => o.ID == id);
        }

        public virtual void Save(T itemToSave)
        {           
            if (itemToSave.ID == 0)
            {
                itemToSave.CreatedBy = ActiveUser;
                itemToSave.Created = DateTime.Now;
                DbContext.Entry(itemToSave).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                itemToSave.Updated = DateTime.Now;
                itemToSave.UpdatedBy = ActiveUser;
                DbContext.Entry(itemToSave).State = System.Data.Entity.EntityState.Modified;
            }
            SaveChanges();
        }
        public virtual async Task SaveAsync(T itemToSave)
        {
            if (itemToSave.ID == 0)
            {
                itemToSave.CreatedBy = ActiveUser;
                itemToSave.Created = DateTime.Now;
                DbContext.Entry(itemToSave).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                itemToSave.Updated = DateTime.Now;
                itemToSave.UpdatedBy = ActiveUser;
                DbContext.Entry(itemToSave).State = System.Data.Entity.EntityState.Modified;
            }
            await SaveChangesAsync();
        }

        public virtual void Delete(T itemToSave)
        {
            if (itemToSave.ID != 0)
            {
                itemToSave.IsDeleted = true;
                itemToSave.Deleted = DateTime.Now;
                itemToSave.DeletedBy = ActiveUser;
                DbContext.Entry(itemToSave).State = System.Data.Entity.EntityState.Modified;
                SaveChanges();
            }            
        }
        public virtual async Task DeleteAsync(T itemToSave)
        {
            if (itemToSave.ID != 0)
            {
                itemToSave.IsDeleted = true;
                itemToSave.Deleted = DateTime.Now;
                itemToSave.DeletedBy = ActiveUser;
                DbContext.Entry(itemToSave).State = System.Data.Entity.EntityState.Modified;
                await SaveChangesAsync();
            }
        }

        //protected virtual bool GetFieldByName(T obj, M id, string idFieldName)
        //{
        //    return id.Equals(GetIdFieldValue(obj, idFieldName));
        //}

        //protected virtual M GetIdFieldValue(T obj, string idFieldName)
        //{
        //    var propertyInfo = typeof(T).GetProperties().FirstOrDefault(o => o.Name == idFieldName);
        //    if (propertyInfo == null) throw new WrongPropertyIdNameException();
        //    return (M)propertyInfo.GetValue(obj);
        //}

        public virtual IQueryable<T> All()
        {
            return DbContext.Set<T>();
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            //DbContext.BulkInsert(entities);
            EnsureOpenConnection();          
            using (var bulkCopy = new SqlBulkCopy((SqlConnection)DbContext.Database.Connection)
            {
                DestinationTableName = GetTableName<T>(),
            })
            {
                foreach (var mapping in GetColumnMappings<T>())
                {
                    bulkCopy.ColumnMappings.Add(mapping);
                }

                bulkCopy.WriteToServer(entities.ToDataTable());
                
            }
            //if (entities.Any())
            //    DbContext.
            RefreshContext();
            //SaveChanges();
        }
        private void EnsureOpenConnection()
        {
            if (((SqlConnection)DbContext.Database.Connection).State == ConnectionState.Closed)
            {
                ((SqlConnection)DbContext.Database.Connection).Open();
            }
        }
        public void Insert(T entity)
        {
            DbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
            SaveChanges();
        }
        private void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                RefreshContext();
                DbContext.SaveChanges();
            }
        }
        private async Task SaveChangesAsync()
        {
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (OptimisticConcurrencyException)
            {
                await RefreshContextAsync();
                await DbContext.SaveChangesAsync();
            }
        }
        private void RefreshContext()
        {
            var ctx = ((IObjectContextAdapter)DbContext).ObjectContext;
            ctx.Refresh(System.Data.Entity.Core.Objects.RefreshMode.ClientWins, DbContext.Set<T>());
        }

        private async Task RefreshContextAsync()
        {
            var ctx = ((IObjectContextAdapter)DbContext).ObjectContext;
            await ctx.RefreshAsync(System.Data.Entity.Core.Objects.RefreshMode.ClientWins, DbContext.Set<T>());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        private string GetTableName<S>() where S : class
        {
            string sql = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<S>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);

            string table = match.Groups["table"].Value;
            return table;
        }

        private IEnumerable<SqlBulkCopyColumnMapping> GetColumnMappings<S>()
        {
            var methadata = ((EntityConnection)((IObjectContextAdapter)DbContext).ObjectContext.Connection).GetMetadataWorkspace();
            var entitySet = methadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                      .Single()
                      .EntitySets
                      .Single(s => s.ElementType.Name == typeof(S).Name);
            var mapping = methadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                          .Single()
                          .EntitySetMappings
                          .Single(s => s.EntitySet == entitySet);
            var columns = mapping.EntityTypeMappings.Single()
                .Fragments.Single()
                .PropertyMappings
                .OfType<ScalarPropertyMapping>();
            return columns.Select(o => new SqlBulkCopyColumnMapping(o.Property.Name, o.Column.Name));
        }
    }
}
