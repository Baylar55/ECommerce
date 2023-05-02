import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { UserService } from 'src/app/services/common/models/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{

  constructor(private formBuilder: FormBuilder, private userService: UserService, private customToastrService:CustomToastrService) { }

  registerForm:FormGroup

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
    nameSurname:["", [Validators.required, Validators.maxLength(40), Validators.minLength(3)]],
    username:["", [Validators.required, Validators.maxLength(40), Validators.minLength(5)]],
    email:["", [Validators.required, Validators.maxLength(40), Validators.email]],
    password:["", [Validators.required]],
    repeatPassword:["", [Validators.required]]
    },
    {validators:(group: AbstractControl) : ValidationErrors | null => 
      {
       let password = group.get("password").value;
       let repeatPassword = group.get("repeatPassword").value;
       return password === repeatPassword ? null : { notSame: true }; 
      }})    
  }

  get component(){
    return this.registerForm.controls;
  }

  submitted: boolean = false;
  async onSubmit(user: User){
    this.submitted=true;

    if (this.registerForm.invalid) 
      return;
    
    const result: Create_User = await this.userService.create(user);
    if(result.isSucceeded)
      this.customToastrService.message(result.message,"User registration succeeded",{
        messageType:ToastrMessageType.Success,
        position:ToastrPosition.TopRight
      })
    else{
      this.customToastrService.message(result.message,"Error",{
        messageType:ToastrMessageType.Error,
        position:ToastrPosition.TopRight 
      })
    }
  }
}
