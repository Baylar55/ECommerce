import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateProduct } from 'src/app/contracts/Create_Product';
import { HttpErrorResponse } from '@angular/common/http';
import { List_Product } from 'src/app/contracts/list_product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: CreateProduct, successCallBack?: ()=> void, errorCallBack?: (errorMessage: string)=> void){
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

  async read(page: number = 0, size: number = 5, successCallBack?: ()=> void, errorCallBack?: (errorMessage: string)=> void): Promise<{totalCount: number, products: List_Product[]}>{
    const promiseData: Promise<{totalCount: number, products: List_Product[]}> = this.httpClientService.get<{totalCount: number, products: List_Product[]}>({
      controller: "product",
      queryString: `page=${page}&{size}=${size}`
    }).toPromise();

    promiseData.then(d=>successCallBack())
    .catch((errorResponse: HttpErrorResponse)=>errorCallBack(errorResponse.message));

    return await promiseData;
  } 

}
