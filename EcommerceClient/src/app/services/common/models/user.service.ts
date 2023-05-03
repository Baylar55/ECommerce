import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { User } from 'src/app/entities/user';
import { Create_User } from 'src/app/contracts/users/create_user';
import { Observable, firstValueFrom } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { Token } from 'src/app/contracts/token/token';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService: HttpClientService, private toastrService: CustomToastrService) { }
  async create(user: User){
    const observable: Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller:"user",
    },user);

    return await firstValueFrom(observable) as Create_User;
  }

  async login(usernameOrEmail: string, password: string, callBackFunction?: ()=> void ): Promise<any>{
    const observable: Observable<any | TokenResponse  > = this.httpClientService.post<any | TokenResponse >({
      controller:"user",
      action:"login" 
    },{
      usernameOrEmail, password
    })
  const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
  
  if(tokenResponse){
    localStorage.setItem("accessToken", tokenResponse.token.accessToken);

    this.toastrService.message("Authentication is succeeded","Authenticated",{
      messageType:ToastrMessageType.Success,
      position:ToastrPosition.TopRight
    });
  }
    callBackFunction();
  }
}
