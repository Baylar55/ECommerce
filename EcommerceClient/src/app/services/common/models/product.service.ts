import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateProduct } from 'src/app/contracts/Create_Product';
import { HttpErrorResponse } from '@angular/common/http';
import { List_Product } from 'src/app/contracts/list_product';
import { Observable, firstValueFrom } from 'rxjs';
import { List_Product_Image } from 'src/app/contracts/list_product_image';

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

  async read(page: number = 0, size: number = 5, successCallBack?: ()=> void, errorCallBack?: (errorMessage: string)=> void): Promise<{totalProductCount: number, products: List_Product[]}>{
    const promiseData: Promise<{totalProductCount: number, products: List_Product[]}> = this.httpClientService.get<{totalProductCount: number, products: List_Product[]}>({
      controller: "product",
      queryString: `page=${page}&{size}=${size}`
    }).toPromise();

    promiseData.then(d=>successCallBack())
    .catch((errorResponse: HttpErrorResponse)=>errorCallBack(errorResponse.message));

    return await promiseData;
  } 

  async delete(id:string){
    const deleteObservable : Observable<any> = this.httpClientService.delete<any>({
      controller:"product"
    },id);
    await firstValueFrom(deleteObservable);
  }
  
  async readImages(id:string,successCallBack?: ()=> void): Promise<List_Product_Image[]>{
    const getObservable:Observable<List_Product_Image[]>= this.httpClientService.get<List_Product_Image[]>({
      action:"GetProductImages",
      controller:"product",

    },id)
    const images: List_Product_Image[] = await firstValueFrom(getObservable);
    successCallBack();
    return images;
  } 

  async deleteImage(id: string, imageId: string, successCallBack?: ()=> void){
    const deleteObservable = this.httpClientService.delete({
      action:"deleteproductimage",
      controller:"product",
      queryString:`imageId=${imageId}`
    },id)
    await firstValueFrom(deleteObservable);
    successCallBack();
  }

  async changeShowcaseImage(imageId: string, productId: string, successCallBack?: ()=> void):Promise<void>{
    const changeShowcaseImageObservable = this.httpClientService.get({
      controller:"product",
      action:"ChangeShowcaseImage",
      queryString:`imageId=${imageId}$productId=${productId}`,
    });
    await firstValueFrom(changeShowcaseImageObservable);  
    successCallBack();
  }
}
