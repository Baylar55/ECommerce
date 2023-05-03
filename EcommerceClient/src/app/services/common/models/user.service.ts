import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { User } from 'src/app/entities/user';
import { Create_User } from 'src/app/contracts/users/create_user';
import { Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService: HttpClientService ) { }
  async create(user: User){
    const observable: Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller:"user",
    },user);

    return await firstValueFrom(observable) as Create_User;
  }

  async login(usernameOrEmail: string, password: string, callBackFunction?: ()=> void ): Promise<void>{
    const observable: Observable<any> = this.httpClientService.post({
      controller:"user",
      action:"login" 
    },{
      usernameOrEmail, password
    })
    return await firstValueFrom(observable);
    callBackFunction();
  }

}
