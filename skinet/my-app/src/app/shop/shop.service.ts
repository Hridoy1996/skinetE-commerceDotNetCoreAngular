import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { Ipagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { map } from 'rxjs/operators';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }
  getProducts(shopParams: ShopParams){
    let params = new HttpParams();
    params.append('pageSize','sdf');
    return  this.http.get<Ipagination>(this.baseUrl +
       'products/GetProductsWithPaggination', { observe: 'response', params})
       .pipe(
         map(response =>{
           return response.body;
         })
       );
  }
  getProduct(id: string){
    return  this.http.get<IProduct>(this.baseUrl +
       'Products/GetProduct/' + id);
  }
  getProductBrands(){
    return  this.http.get<IBrand[]>(this.baseUrl +
       'Products/GetProductBrands')
  }
  getProductTypes(){  
    return  this.http.get<IProductType[]>(this.baseUrl +
       'Products/GetProductTypes')
  }
}
