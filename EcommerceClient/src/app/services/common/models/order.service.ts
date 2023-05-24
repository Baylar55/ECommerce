import { Injectable } from '@angular/core';
import { HttpClientService, RequestParameters } from '../http-client.service';
import { Create_Order } from 'src/app/contracts/order/create_order';
import { Observable, firstValueFrom } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClientService: HttpClientService) { }

  async create(order: Create_Order): Promise<void> {

    console.log(order);
    const observable: Observable<any> = this.httpClientService.post({
      controller: "Orders"
    }, order);

    await firstValueFrom(observable);
  }
}
