import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
const API_URL = '<<URL_HERE>>';
@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {}

  getKeywords(): Observable<any> {
    return this.http.get(API_URL + 'getcable', {
      responseType: 'text',
    });
  }
}
