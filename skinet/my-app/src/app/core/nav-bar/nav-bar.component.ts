import { Component, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { Observable} from 'rxjs';
import { IUser } from 'src/app/shared/models/user';
import { AccountService } from 'src/app/account/account.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  currentUser$: Observable<IUser>;

  constructor(private basketService: BasketService, private accountSerive: AccountService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountSerive.currentUser$;
   
  }
  logout(){
    this.accountSerive.logout();
  }

}
