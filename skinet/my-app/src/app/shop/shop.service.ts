import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { Ipagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams){
    let params = new HttpParams();

    if(shopParams.brandId !== '0'){
      params =  params.append('BrandId', shopParams.brandId.toString());
    }
    if(shopParams.typeId !== '0'){
      params =  params.append('TypeId', shopParams.typeId.toString());
    }
   
      params =  params.append('PageIndex', shopParams.pageNumber.toString());
      params =  params.append('PageSize', shopParams.pageSize.toString());
      params = params.append('Sort', shopParams.sort);
    
   
    return this.http.get<Ipagination>(this.baseUrl +
       'products/GetProductsWithPaggination', {observe: 'response', params})
       .pipe(    
        map(response => {
           return response.body;
         })
       )
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
