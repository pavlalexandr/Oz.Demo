import { Component } from "@angular/core";
import { RegionModel, RegionService } from "../../services/region.service";
import { Location } from '@angular/common';
import { Router } from "@angular/router";
//import * as universal from "univers
@Component({
    selector: "create",
    template: require('./create.component.html'),
    providers: [RegionService]
})
export class CreateComponent {
    public model: RegionModel;

    constructor(private regionService: RegionService, private location: Location, private router: Router) {
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