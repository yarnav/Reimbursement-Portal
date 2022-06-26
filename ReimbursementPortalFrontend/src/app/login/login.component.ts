import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule, FormControlDirective } from '@angular/forms';
import { SharedServiceService } from '../Shared/shared-service.service';
import { Route, Router } from '@angular/router'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  LoginForm = new FormGroup({
    Email: new FormControl('', [Validators.required, Validators.email]),
    Password: new FormControl('', [Validators.required,
    Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/),
    Validators.minLength(8)])
  })
  Alert = false;
  constructor(private service: SharedServiceService, private router: Router) { }

  ngOnInit(): void {
  }

  GetFormValues() {
    this.LoggingIn(this.LoginForm.value)
  }

  LoggingIn(Credentials: any) {
    this.service.Login(Credentials).subscribe(result => {
      if (result) {
        this.service.LoggedInUserData = result;
        console.warn("LoggedInUserData.isApprover", this.service.LoggedInUserData.isApprover);
        this.Alert = false;
        if (this.service.LoggedInUserData.isApprover) {
          this.router.navigate(['/Pending'])
        }
        else {

          this.router.navigate(['/Employee'])
        }
      }
      else {
        this.Alert = true;
        this.LoginForm.reset({});
        console.warn("Login returned null");
      }
    })
  }

  CloseAlert() {
    this.Alert = false;
  }

  get Email() {
    return this.LoginForm.get('Email');
  }
  get Password() {
    return this.LoginForm.get('Password');
  }
}
