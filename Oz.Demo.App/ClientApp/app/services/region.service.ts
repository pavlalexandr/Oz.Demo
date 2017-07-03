import { Injectable, Inject  } from "@angular/core";
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
//import { InjectionToken } from '@angular/core';
//export const ORIGIN_URL = new InjectionToken<string>('ORIGIN_URL');
@Injectable()
export class RegionService {
    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) {

    }
    getRegionsbyName(nameSort?: boolean): Observable<RegionModel[]>{
        //console.log(this.originUrl);
        if (nameSort === null || nameSort === undefined) {
            return this.http.get(this.originUrl+"/api/Region").map(this.extractData);
        } else {
            return this.http.get(this.originUrl+"/api/Region?nameSort=" + nameSort).map(this.extractData);
        }
    }
    createRegion(model: RegionModel): Observable<any> {
        return this.http.post(this.originUrl + "/api/Region", model).map(o => o.toString());
    }
    deleteRegion(id: number): Observable<any> {
        return this.http.delete(this.originUrl + "/api/Region/" + id).map(o => o.toString());
    }
    getById(id: any): Observable<RegionModel> {
        return this.http.get(this.originUrl + "/api/Region/" + id).map(this.extractData);
    }
    private extractData(res: Response) {
        return res.json();
    }

    private handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        //let errMsg: string;
        //if (error instanceof Response) {
        //    const body = error.json() || '';
        //    const err = body.error || JSON.stringify(body);
        //    errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        //} else {
        //    errMsg = error.message ? error.message : error.toString();
        //}
        console.error(error.toString());
        return Observable.throw(error.toString());
    }
}
export interface RegionModel {
    id: number;
    name: string;
    timeZone: string;
}