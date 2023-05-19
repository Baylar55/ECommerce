import { Component, Inject, OnInit, Output } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { List_Product_Image } from 'src/app/contracts/list_product_image';
import { ProductService } from 'src/app/services/common/models/product.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { DialogService } from 'src/app/services/common/dialog.service';

declare var $:any 

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss'],
})

export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit{
  constructor(
    dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string,
    private productService: ProductService,
    private spinner: NgxSpinnerService,
    private dialogService: DialogService) {
    super(dialogRef);
  }

  @Output() options: Partial<FileUploadOptions> = {
    accept: '.png, .jpg, .jpeg, .gif',
    action: 'upload',
    controller: 'product',
    explanation: 'Choose product images...',
    isAdminPage: true,
    queryString: `id=${this.data}`,
  };

  images:List_Product_Image[];

  async ngOnInit() {
    this.spinner.show(SpinnerType.BallAtom);
      this.images = await this.productService.readImages(this.data as string, ()=>{this.spinner.hide(SpinnerType.BallAtom)});
  }

  async deleteImage(imageId: string, event:any) {
    this.spinner.show(SpinnerType.BallAtom)
        await this.productService.deleteImage(this.data as string, imageId, () => {
          this.spinner.hide(SpinnerType.BallAtom);
          var card = $(event.srcElement).parent().parent().parent();
          card.fadeOut(500);
        });
  }

  showCase(imageId: string){
    this.spinner.show(SpinnerType.BallAtom);
    this.productService.changeShowcaseImage(imageId, this.data.toString(), ()=>{
      this.spinner.hide(SpinnerType.BallAtom);
    });
  }
}

export enum SelectProductImageState {
  Close,
}
