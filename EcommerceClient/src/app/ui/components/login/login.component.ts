import { FacebookLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable, firstValueFrom } from 'rxjs';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { AuthService } from 'src/app/services/common/auth.service';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
import { UserService } from 'src/app/services/common/models/user.service';
import { CustomToastrService } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent{
  constructor(private userAuthService: UserAuthService, spinner:NgxSpinnerService, private authService:AuthService, private activatedRoute:ActivatedRoute, private router: Router, private socialAuthService:SocialAuthService, private httpClientService:HttpClientService)
  { 
    super(spinner)
    socialAuthService.authState.subscribe(async(user:SocialUser)=>{
      console.log(user);
      
      this.showSpinner(SpinnerType.BallAtom);
     switch(user.provider){
      case "Google":
        await userAuthService.googleLogin(user, ()=>{                    
        this.authService.identityCheck();
        this.hideSpinner(SpinnerType.BallAtom);
        })
        break;
      case "Facebook":
        await userAuthService.facebookLogin(user,()=>{       
        this.authService.identityCheck();
        this.hideSpinner(SpinnerType.BallAtom);
        })
        break;
     }
    });
    
  }

  async login(usernameOrEmail:string, password: string){
    this.showSpinner(SpinnerType.BallAtom);
    await this.userAuthService.login(usernameOrEmail, password, ()=>
    {
      this.authService.identityCheck();

      this.activatedRoute.queryParams.subscribe(params=>{
        const returnUrl: string = params["returnUrl"];
        if(returnUrl)
          this.router.navigate([returnUrl]);
      });

      this.hideSpinner(SpinnerType.BallAtom)
    });
  }

  facebookLogin() {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }
}
