import { Injectable } from '@angular/core';
import { HttpClientService, RequestParameters } from '../http-client.service';
import { Create_Order } from 'src/app/contracts/order/create_order';
import { Observable, firstValueFrom } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { List_Order } from 'src/app/contracts/order/list_order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClientService: HttpClientService) { }

  async create(order: Create_Order): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post({
      controller: "Orders"
    }, order);

    await firstValueFrom(observable);
  }

  async getAllOrders(page: number = 0, size: number = 8, successCallBack?: ()=> void, errorCallBack?: (errorMessage: string)=> void): Promise<{totalOrderCount: number, orders: List_Order[]}> {
    const observable: Observable<{totalOrderCount: number, orders: List_Order[]}> = this.httpClientService.get({
      controller: "Orders",
      queryString:  `page=${page}&{size}=${size}`
    });

    const promiseData = firstValueFrom(observable);
    promiseData.then(value=>successCallBack())
      .catch(error=>errorCallBack(error));

    return await promiseData;
  }
}
