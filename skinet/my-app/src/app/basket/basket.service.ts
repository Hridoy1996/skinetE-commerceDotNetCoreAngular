import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {Basket, IBasket, IBasketItem, IBasketTotals} from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private baseketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.baseketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string){
    return this.http.get(this.baseUrl + 'basket?id='+ id)
    .pipe(
      map(( basket: IBasket) =>{
        this.baseketSource.next(basket);
        this.calculateTotals(); 
      })
    );
  }
  incrementItemQuanity(item: IBasketItem){
    const basket = this.getCurrentbasketValue();
    const foundItemIndex = basket.items.findIndex( x => x.id === item.id);
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }
  decrementItemQuanity(item: IBasketItem){
    const basket = this.getCurrentbasketValue();
    const foundItemIndex = basket.items.findIndex( x => x.id === item.id);
    if(basket.items[foundItemIndex].quantity>1){
    basket.items[foundItemIndex].quantity--;
    this.setBasket(basket);
    }
    else{
      this.removeItemFromBasket(item);
    }
  }
   removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentbasketValue();
    if( basket.items.some( x=> x.id === item.id)){
      basket.items = basket.items.filter(i => i.id !== item.id);
      if(basket.items.length>0){
        this.setBasket(basket);
      } else{
        this.deleteBasket(basket);
      }
    }
  }
  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe(() => {
      this.baseketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
  }
  calculateTotals(){
    const basket = this.getCurrentbasketValue();
    const shipping = 0;
    const subTotal = basket.items.reduce((a,b) => (b.price *b.quantity) + a, 0);
    const total = subTotal + shipping;
    this.basketTotalSource.next({shipping, total, subTotal});
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) =>{
      this.baseketSource.next(response);
      this.calculateTotals();
    }, error => {
      console.log(error);
    });
  }
  getCurrentbasketValue(){
    return this.baseketSource.value;
  }
  private createBasket(): IBasket{
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id); 
    return basket;
  }

  addItemToBasket(item: IProduct, quantity=1){
    const itemToAdd: IBasketItem= this.mapProductionItemToBasketItem(item, quantity);
    const basket = this.getCurrentbasketValue() ?? this.createBasket(); 
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }
  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    
    const index = items.findIndex(i => i.id === itemToAdd.id);
    if( index === -1){
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    else{
      items[index].quantity += quantity;
    }
    return items;
  }
  private mapProductionItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return{
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity, 
      brand: item.productBrand,
      type: item.productType
    };
  }
}
