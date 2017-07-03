import { Component, OnInit, Inject } from '@angular/core';
import { RegionModel, RegionService } from '../../services/region.service';
import { PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';
import { Router } from "@angular/router";
//import * as alertify from 'alertifyjs';
@Component({
    selector: 'home',
    template: require('./home.component.html'),
    providers: [RegionService]
})
export class HomeComponent implements OnInit {
    public settings = {
        columns: {
            name: {
                title: 'Name'
            },
            timeZone: {
                title: 'Time Zone'
            }
        }
    };
    public rowData: RegionModel[];
    public sort?: boolean;
    constructor(private regionService: RegionService, @Inject(PLATFORM_ID) private platformId: Object, private router:Router) {

    }
    ngOnInit(): void {
        this.regionService.getRegionsbyName().subscribe(o =>
            this.rowData = o);

    }
    onSort(): void {
        if (this.sort === undefined) {
            this.sort = true;
        } else if (this.sort) {
            this.sort = false;
        } else {
            this.sort = undefined;
        }
        this.regionService.getRegionsbyName(this.sort).subscribe(o =>
            this.rowData = o);
    }

    onDelete(id: number): void {
        if (isPlatformBrowser(this.platformId)) {
            //var alertify = require("alertifyjs");
            //alertify.confirm("Warning","Are you sure you want delete it?", function (result) {
                
            //});
            if (confirm("Are you sure you want delete it?")) {
                this.regionService.deleteRegion(id).subscribe(o => {
                    this.regionService.getRegionsbyName(this.sort).subscribe(o =>
                        this.rowData = o);
                });
            }
        }
    }

    onEdit(id: number): void {
        this.router.navigate(['/editRegion',id]);
    }

}
