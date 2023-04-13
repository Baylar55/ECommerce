import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { CreateProduct } from 'src/app/contracts/Create_Product';
import { ListComponent } from './list/list.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {
  constructor(spinner : NgxSpinnerService) {
    super(spinner);
  }
ngOnInit(): void {
    
 }

 @ViewChild(ListComponent) listComponents: ListComponent;

 createdProduct(createdProduct: CreateProduct){
  this.listComponents.getProducts();
 }
}
