import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomersComponent } from './customers.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    CustomersComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path:"", component:CustomersComponent} 
      //If this module receives a request called customer(for ex.), it should handle CustomersComponent. If we have more component related with customer, we will declare the path and route of each.If we have only one component we don't need to write path, we can keep empty it.
    ])
  ]
})
export class CustomersModule { }
