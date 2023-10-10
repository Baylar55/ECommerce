import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { CreateProduct } from 'src/app/contracts/Create_Product';
import { ListComponent } from './list/list.component';
import { DialogService } from 'src/app/services/common/dialog.service';
import { QrcodeDialogComponent } from 'src/app/dialogs/qrcode-dialog/qrcode-dialog.component';
import { QrcodeReadingDialogComponent } from 'src/app/dialogs/qrcode-reading-dialog/qrcode-reading-dialog.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {
  constructor(spinner : NgxSpinnerService, private dialogService:DialogService) {
    super(spinner);
  }
ngOnInit(): void {
    
 }

 @ViewChild(ListComponent) listComponents: ListComponent;

 createdProduct(createdProduct: CreateProduct){
  this.listComponents.getProducts();
 }

 showProductQRCodeReading(){
  this.dialogService.openDialog({
  componentType:QrcodeReadingDialogComponent,
  data: null,
  options:{
    width:"1000px"
  },
  afterClosed: ()=>{}
  });
 }
}
