import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

const API_URL = 'https://swe.sowiho.de';
//const API_URL = 'localhost:5000';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {
  }

  private static toURL(path : string, query? : string, fragment? : string) {
    let url =  `${API_URL}/${path}`

    if (query != undefined) {
      url = `${url}?${query}`
    }

    if (fragment != undefined) {
      url = `${url}#${fragment}`
    }

    return url;
  }

  getCableList(): Observable<CableList> {
    return this.http.get<CableList>(DataService.toURL("getcablefilelist"), {
      responseType: 'json'
    })
  }

  getCable(cableName: string) {
    return this.http.get<CableInfo>(DataService.toURL("getcable", `cablename=${cableName}`), {
      responseType: 'json'
    })
  }
}

export type CableList = string[];

export type CableInfo = object; //todo
