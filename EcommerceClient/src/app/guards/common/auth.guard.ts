import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { SpinnerType } from 'src/app/base/base.component';
import { MessageType } from 'src/app/services/admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, private router: Router, private toastrService: CustomToastrService, private spinner: NgxSpinnerService) {}

  canActivate( route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    this.spinner.show(SpinnerType.BallAtom)
    const token: string = localStorage.getItem("accessToken");

    let expired: boolean;
    try {
      expired = this.jwtHelper.isTokenExpired(token);
    } catch (error) {
      expired = true;
    }
    
    if(!token || expired){
      this.router.navigate(['login'], { queryParams: { returnUrl: state.url } });
      this.toastrService.message("You must login", "Unauthorized access",{
        messageType:ToastrMessageType.Warning,
        position:ToastrPosition.TopRight
      })
    }
    this.spinner.hide(SpinnerType.BallAtom);
    return true;
  }
  
}
