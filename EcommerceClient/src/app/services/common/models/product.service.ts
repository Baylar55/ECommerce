import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateProduct } from 'src/app/contracts/Create_Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: CreateProduct, successCallBack?:any){
    this.httpClientService
      .post(
        {
          controller: 'product',
        },
        product)
      .subscribe((result) => {
        successCallBack();
      }); 
  }
}
