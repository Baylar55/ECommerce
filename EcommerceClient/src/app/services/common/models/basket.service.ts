import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { List_Basket_Item } from 'src/app/contracts/basket/list-basket-item';
import { Observable, firstValueFrom } from 'rxjs';
import { Create_Basket_Item } from 'src/app/contracts/basket/create-basket-item';
import { UpdateBasketItem } from 'src/app/contracts/basket/update-basket-item';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  constructor(private httpClientService: HttpClientService) {}

  async get(): Promise<List_Basket_Item[]> {
    const observable: Observable<List_Basket_Item[]> =
      this.httpClientService.get({
        controller: "basket",
      });

    return await firstValueFrom(observable);
  }

  async add(basketItem: Create_Basket_Item): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post({ 
        controller: "basket",
        action: "addItemToBasket"
      },basketItem );

    await firstValueFrom(observable);
  }

  async updateQuantity(basketItem: UpdateBasketItem): Promise<void>{
    const observable: Observable<any> = this.httpClientService.put({
      controller:"basket",
    }, basketItem);

    await firstValueFrom(observable);
  }

  async remove(basketItemId: string){
    const observable: Observable<any> = this.httpClientService.delete({
      controller:"basket",
    }, basketItemId);

    await firstValueFrom(observable);
  }
}
