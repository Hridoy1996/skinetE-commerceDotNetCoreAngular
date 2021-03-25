import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { IProduct } from '../shared/models/product';
import { IProductType } from '../shared/models/productType';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search', {static: true}) searchTerm: ElementRef;

  products: IProduct[];
  types: IProductType[];
  brands: IBrand[]; 
  shopParams = new ShopParams();
  totalCount: number;
  sortOptions = [
    {name: 'Alphabetical', value : 'desc'},
    {name: 'Price: Low to High', value : 'priceAsc'},
    {name: 'Price: High to Low', value : 'priceDesc'}
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe(response =>{
      this.products = response.data;
      this.totalCount = response.count;
      
    }, error => {
      console.log(error);
    });
  }
  getBrands(){
    this.shopService.getProductBrands().subscribe(response =>{
      this.brands = [{id: "0", name :'all'}, ...response];
      
    }, error => {
      console.log(error);
    });
  }
  getTypes(){
    this.shopService.getProductTypes().subscribe(response =>{
      this.types = [{id: "0", name :'all'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: string){
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onTypeSelected(typeId: string){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1; 
    this.getProducts();
  }
  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }
  onPageChanged(event: any){
    if(this.shopParams.pageNumber !== event)
    {
    this.shopParams.pageNumber = event;
    this.getProducts();
    }
  }
  onSearch()
  {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onReset(){
    this.searchTerm.nativeElement.value = 'undefined';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
