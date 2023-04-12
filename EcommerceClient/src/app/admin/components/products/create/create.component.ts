import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { CreateProduct } from 'src/app/contracts/Create_Product';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent extends BaseComponent implements OnInit {
  constructor(spinner: NgxSpinnerService ,private productService: ProductService, private alertify:AlertifyService) {
    super(spinner)
  }
  ngOnInit(): void {}

  create(name: HTMLInputElement, stock: HTMLInputElement, price:HTMLInputElement) {

    this.showSpinner(SpinnerType.BallAtom);

    const createProduct: CreateProduct = new CreateProduct();
    createProduct.name = name.value;
    createProduct.stock = parseInt(stock.value);
    createProduct.price = parseFloat(price.value);

    this.productService.create(createProduct, () =>{
      this.hideSpinner(SpinnerType.BallAtom);
      this.alertify.message("Product succesfully added", {
        dismissOthers:true,
        messageType: MessageType.Success,
        position:Position.TopRight
      }) 
    });
  }
}
