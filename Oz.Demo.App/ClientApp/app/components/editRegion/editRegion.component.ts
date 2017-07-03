import { Component, OnInit } from "@angular/core";
import { RegionModel, RegionService } from "../../services/region.service";
import { Location } from '@angular/common';
import { Router, ActivatedRoute, ParamMap } from "@angular/router";
import 'rxjs/add/operator/switchMap';
//import * as universal from "univers
@Component({
    selector: "editRegion",
    template: require('./editRegion.component.html'),
    providers: [RegionService]
})
export class EditRegionComponent implements OnInit {
    ngOnInit(): void {
        this.route.paramMap
            .switchMap((params: ParamMap) =>
                this.regionService.getById(params.get('id')))
            .subscribe((hero: RegionModel) => this.model = hero);
    }

    public model: RegionModel;

    constructor(private regionService: RegionService, private location: Location, private router: Router, private route: ActivatedRoute) {
        this.model = {
            name: "",
            timeZone: "",
            id: 0
        };
    }

    public onBackClick(): void {
        this.location.back();
    }

    public onSubmit(): void {
        this.regionService.createRegion(this.model).subscribe(o => {
            this.router.navigate(["/home"]);
        });
    }
}