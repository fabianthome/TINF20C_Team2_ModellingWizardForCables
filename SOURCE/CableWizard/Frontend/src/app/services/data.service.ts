import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {ProductDetails} from '../models/product-details';
import {environment} from "../../environments/environment";

const API_URL = environment.production ? 'https://swe.sowiho.de/api/v2' : 'https://localhost:7200/api/v2';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {
  }

  private static toURL(path: string, query?: string, fragment?: string) {
    let url = `${API_URL}/${path}`;

    if (query != undefined) {
      url = `${url}?${query}`;
    }

    if (fragment != undefined) {
      url = `${url}#${fragment}`;
    }

    return url;
  }

  getProductList(): Observable<string[]> {
    return this.http.get<string[]>(DataService.toURL('products'), {
      responseType: 'json',
    });
  }

  getProductDetails(productId: string): Observable<ProductDetails> {
    return this.http.get<ProductDetails>(
      DataService.toURL(`product-details/${productId}`),
      {
        responseType: 'json',
      }
    );
  }

  deleteProductDetails(productId: string) {
    return this.http.delete(DataService.toURL(`delete-product/${productId}`), {
      responseType: 'json',
    });
  }

  postProductDetails(product: ProductDetails) {
    return this.http.post(DataService.toURL(`edit-product`), product, {
      responseType: 'json',
    });
  }

  createProduct(product: ProductDetails): Observable<any> {
    return this.http.post(DataService.toURL(`create-product`), product, {
      responseType: 'json',
    });
  }

  getPossibleConnectors(): Observable<any> {
    return this.http.get<string[]>(DataService.toURL('possible-connectors'), {
      responseType: 'json',
    });
  }

  /**
   * @param  {number} ms
   */
  delay(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms));
  }

  getFile(caexVersion: string): Observable<any> {
    return this.http.get(DataService.toURL(`download-file/${caexVersion}`), {
      responseType: 'blob',
    });
  }
}
