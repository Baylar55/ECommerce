import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
declare var $:any
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'EcommerceClient';
  constructor() {
   
  }
}
// $.get("https://localhost:7238/api/Product", data=>{console.log(data);
// })


