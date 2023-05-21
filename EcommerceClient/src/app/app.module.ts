import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './ui/components/login/login.component';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule, GoogleSigninButtonModule, FacebookLoginProvider} from '@abacritt/angularx-social-login';
import { HttpErrorHandlerInterceptorService } from './services/common/http-error-handler-interceptor.service';
import { BasketsModule } from "./ui/components/baskets/baskets.module";
@NgModule({
    declarations: [
        AppComponent,
        LoginComponent
    ],
    providers: [
        { provide: "baseUrl", useValue: "https://localhost:7238/api", multi: true },
        {
            provide: "SocialAuthServiceConfig",
            useValue: {
                autoLogin: false,
                providers: [
                    {
                        id: GoogleLoginProvider.PROVIDER_ID,
                        provider: new GoogleLoginProvider("161723639845-2gfellhshgtmev8ripl7onlah2smprc0.apps.googleusercontent.com")
                    },
                    {
                        id: FacebookLoginProvider.PROVIDER_ID,
                        provider: new FacebookLoginProvider("546631843676576")
                    }
                ],
                onError: err => console.log(err)
            } as SocialAuthServiceConfig
        },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorHandlerInterceptorService, multi: true }
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        AdminModule,
        UiModule,
        ToastrModule.forRoot(),
        NgxSpinnerModule,
        HttpClientModule,
        JwtModule.forRoot({
            config: {
                tokenGetter: () => localStorage.getItem("accessToken"),
                allowedDomains: ["localhost:7238"],
            }
        }),
        SocialLoginModule,
        GoogleSigninButtonModule,
        BasketsModule
    ]
})
export class AppModule { }
