import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { QrCodeService } from 'src/app/services/common/qr-code.service';
import { NgxScannerQrcodeComponent } from 'ngx-scanner-qrcode';
import { MatButton } from '@angular/material/button';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';
import { MessageType } from 'src/app/services/admin/alertify.service';
import { ProductService } from 'src/app/services/common/models/product.service';
import { SpinnerType } from 'src/app/base/base.component';

declare var $: any;

@Component({
  selector: 'app-qrcode-reading-dialog',
  templateUrl: './qrcode-reading-dialog.component.html',
  styleUrls: ['./qrcode-reading-dialog.component.scss'],
})
export class QrcodeReadingDialogComponent
  extends BaseDialog<QrcodeReadingDialogComponent>
  implements OnInit
{
  constructor(
    dialogRef: MatDialogRef<QrcodeReadingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
    private spinner: NgxSpinnerService,
    private toastrService: CustomToastrService,
    private productService: ProductService
  ) {
    super(dialogRef);
  }

  @ViewChild('scanner', { static: true }) scanner: NgxScannerQrcodeComponent;
  @ViewChild('txtStock', { static: true }) txtStock: ElementRef;

  ngOnInit() {
    this.scanner.start();
  }
  ngOnDestroy() {
    this.scanner.stop();
  }

  onEvent(e){
    this.spinner.show(SpinnerType.BallAtom);
    const data: any = (e as { data: string }).data;
    if (data != null && data != '') {
      const jsonData = JSON.parse((e as { data: string }).data);
      const stockValue = (this.txtStock.nativeElement as HTMLInputElement).value;     
      this.productService.updateStockQRCodeToProduct(jsonData.Id, parseInt(stockValue),()=>{
        $("#btnClose").click();
        this.toastrService.message(`${jsonData.name}'s stock information updated.`, "Stock successfully updated",{
          messageType:ToastrMessageType.Success,
          position:ToastrPosition.TopRight
        });

        this.spinner.hide(SpinnerType.BallAtom);
      });
    }
  }
}
