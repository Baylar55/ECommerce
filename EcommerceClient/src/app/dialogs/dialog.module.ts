import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { SelectProductImageDialogComponent } from './select-product-image-dialog/select-product-image-dialog.component';
import { FileUploadModule } from '../services/common/file-upload/file-upload.module';
import { FormsModule } from '@angular/forms';
import { BasketItemRemoveDialogComponent } from './basket-item-remove-dialog/basket-item-remove-dialog.component';
import { ShoppingCompleteteDialogComponent } from './shopping-completete-dialog/shopping-completete-dialog.component';



@NgModule({
  declarations: [
    DeleteDialogComponent,
    SelectProductImageDialogComponent,
    BasketItemRemoveDialogComponent,
    ShoppingCompleteteDialogComponent,
  ],
  imports: [ 
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    FileUploadModule,
    MatCardModule,
    FormsModule 
  ]
})
export class DialogModule { }
