import { Component, Inject, OnDestroy } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

declare var $: any;

@Component({
  selector: 'app-shopping-completete-dialog',
  templateUrl: './shopping-completete-dialog.component.html',
  styleUrls: ['./shopping-completete-dialog.component.scss']
})
export class ShoppingCompleteteDialogComponent extends BaseDialog<ShoppingCompleteteDialogComponent> implements OnDestroy{
  constructor(dialogRef: MatDialogRef<ShoppingCompleteteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ShoppingCompleteDeleteState){
   super(dialogRef);
 }

 show: boolean = false;
 complete(){
  this.show = true;
 }

 ngOnDestroy(): void {
  if(!this.show) 
    $('#basketModal').modal('show');
 }
}

export enum ShoppingCompleteDeleteState{
  Yes, 
  No
}

