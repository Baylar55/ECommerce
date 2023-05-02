import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { User } from 'src/app/entities/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{

  constructor(private formBuilder: FormBuilder) { }

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
  onSubmit(data: User){
    this.submitted=true;

    if (this.registerForm.invalid) {
      return;
    }
  }
}
