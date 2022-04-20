import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {ProductInfo} from "../models/product-info";
import {ProductDetails} from "../models/product-details";

const API_URL = 'https://swe.sowiho.de';

//const API_URL = 'localhost:5000';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {
  }

  private static toURL(path: string, query?: string, fragment?: string) {
    let url = `${API_URL}/${path}`

    if (query != undefined) {
      url = `${url}?${query}`
    }

    if (fragment != undefined) {
      url = `${url}#${fragment}`
    }

    return url;
  }

  getProductList(): Observable<string[]> {
    return this.http.get<CableList>(DataService.toURL("getcablefilelist"), {
      responseType: 'json'
    })
    //return this.http.get<string[]>(DataService.toURL("product-list"), {
    //  responseType: 'json'
    //});
  }

  getProductInfo(productId: string): Observable<ProductInfo> {
    return this.http.get<ProductInfo>(DataService.toURL("product-info", `product-id=${productId}`), {
      responseType: 'json'
    });
  }

  getProductDetails(productId: string): Observable<ProductDetails> {
    return this.http.get<ProductDetails>(DataService.toURL("product-details", `product-id=${productId}`), {
      responseType: 'json'
    })
  }

  getFile(path: string): Observable<Blob> {
    return this.http.get(DataService.toURL(path), {
      responseType: 'blob'
    })
  }

  /*
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
  */
}

export type CableList = string[];

export type CableInfo = object; //todo
