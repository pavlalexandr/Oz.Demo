import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CreateComponent } from './components/create/create.component';
import { EditRegionComponent } from './components/editRegion/editRegion.component';
//import { RegionService } from './services/region.service';
//import { Ng2SmartTableModule } from 'ng2-smart-table';
//import { AgGridModule } from "ag-grid-angular/main";
//import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
//import { CounterComponent } from './components/counter/counter.component';

export const sharedConfig: NgModule = {
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        //CounterComponent,
        //FetchDataComponent,
        HomeComponent,
        CreateComponent,
        EditRegionComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'createRegion', component: CreateComponent },
            { path: 'editRegion/:id', component: EditRegionComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        //Ng2SmartTableModule
    ]
};
