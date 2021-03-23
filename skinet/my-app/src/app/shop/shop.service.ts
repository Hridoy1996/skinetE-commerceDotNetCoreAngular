import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { Ipagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/productType';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }
  getProducts(){
    return  this.http.get<Ipagination>(this.baseUrl +
       'products/GetProductsWithPaggination?Sort=desc&BrandId=1&TypeId=1&PageIndex=1&PageSize=2')
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
