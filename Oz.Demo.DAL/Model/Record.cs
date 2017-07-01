using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.DAL.Model
{
    public class Record
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 2.	Дата добавления записи
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 3.	Дата изменения записи
        /// </summary>
        public DateTime? Updated { get; set; }
        /// <summary>
        /// 4.	Дата удаления записи
        /// </summary>
        public DateTime? Deleted { get; set; }
        /// <summary>
        /// 8.	Признак удаления
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 5.	Кто добавил запись
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 6.	Кто изменил запись
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// 7.	Кто удалил запись
        /// </summary>
        public string DeletedBy { get; set; }
    }
}
