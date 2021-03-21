import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { IProduct } from '../shared/models/product';
import { IProductType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  types: IProductType[];
  brands: IBrand[];
  
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts().subscribe(response =>{
      this.products = response.data;
    }, error => {
      console.log(error);
    } 
    )
  }
  getBrands(){
    this.shopService.getProductBrands().subscribe(response =>{
      this.brands = response;
    }, error => {
      console.log(error);
    } 
    )
  }
  getTypes(){
    this.shopService.getProductTypes().subscribe(response =>{
      this.types = response;
    }, error => {
      console.log(error);
    } 
    )
  }

}
