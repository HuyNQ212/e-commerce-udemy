import {Injectable} from '@angular/core';
import {environment} from "../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Product} from "../shared/models/product";
import {Observable} from "rxjs";
import {Type} from "../shared/models/type";
import {Brand} from "../shared/models/brand";
import {Pagination} from "../shared/models/pagination";

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl: string = `${environment.baseUrl}/Products`;

  constructor(private http: HttpClient) {
  }

  getProducts(pageIndex?: number, pageSize?: number,brandId?: number, typeId? : number, sortOrder? : string) {
    let params = new HttpParams();

    if (pageIndex) params = params.append('pageIndex', pageIndex);
    if (pageSize) params = params.append('pageSize', pageSize);
    if (brandId) params = params.append('brandId', brandId);
    if (typeId) params = params.append('typeId', typeId);
    if (sortOrder) params = params.append('sortOrder', sortOrder);

    return this.http.get<Pagination<Product>>(`${this.baseUrl}`, {params: params})
  }

  getProductById(id : number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/`+id)
  }

  getTypes() {
    return this.http.get<Type[]>(`${this.baseUrl}/Types`);
  }

  getBrands() {
    return this.http.get<Brand[]>(`${this.baseUrl}/Brands`);
  }
}
