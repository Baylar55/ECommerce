import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateProduct } from 'src/app/contracts/Create_Product';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: CreateProduct, successCallBack?:any, errorCallBack?: any){
    this.httpClientService
      .post(
        {
          controller: 'product',
        },
        product)
      .subscribe((result) => {
        successCallBack();
      },(errorResponse: HttpErrorResponse)=>{
        const _error: Array<{key: string, value: Array<string>}> = errorResponse.error;
        let message = "";
        _error.forEach((val, ind) => {
          val.value.forEach((value, index)=>{
            message+= `${value}<br>`;
          });
        });
        errorCallBack(message);
      } ); 
  }
}
