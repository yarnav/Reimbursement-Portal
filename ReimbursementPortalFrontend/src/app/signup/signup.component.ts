import { Component, OnInit } from '@angular/core';
import { SharedServiceService } from '../Shared/shared-service.service';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  Registration = new FormGroup({
    Email: new FormControl('', [Validators.required, Validators.email]),
    Password: new FormControl('', [Validators.required,
    Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/),
    Validators.minLength(8)]),
    ConfirmPassword: new FormControl('', [Validators.required,
    Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/),
    Validators.minLength(8)]),
    Name: new FormControl('', [Validators.required, Validators.maxLength(255), Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])/)]),
    PAN: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern(/^(?=.*[A-Z])(?=.*[0-9])/)]),
    Bank: new FormControl('', Validators.required),
    AccountNumber: new FormControl('', [Validators.required, Validators.minLength(12), Validators.maxLength(12), Validators.pattern(/^(?=.*[0-9])/)])
  });
  RegistrationSuccess: any = [];
  EmployeeDetails: any = [];
  Alert = false;
  constructor(private service: SharedServiceService, private router: Router) { }

  ngOnInit(): void {
  }

  CloseAlert() {
    this.Alert = false;
    this.Registration.reset({});
  }

  CreateUser() {
    this.service.PostEmployee(this.Registration.value).subscribe(data => {
      this.RegistrationSuccess = data
      console.warn("Registration Status:", this.RegistrationSuccess);
      if (data) {
        this.Registration.reset({});
        this.Alert = false;
        this.router.navigate(['/Login'])
      }
      else {
        this.Registration.reset({});
        this.Alert = true;
      }
    })
  }

  GetFormValues() {
    console.warn("Values from Registration form ", this.Registration.value);
    this.EmployeeDetails = this.Registration.value;
    this.CreateUser();
  }

  get Email() {
    return this.Registration.get('Email');
  }
  get Password() {
    return this.Registration.get('Password');
  }
  get ConfirmPassword() {
    return this.Registration.get('ConfirmPassword');
  }
  get Name() {
    return this.Registration.get('Name');
  }
  get PAN() {
    return this.Registration.get('PAN');
  }
  get AccountNumber() {
    return this.Registration.get('AccountNumber');
  }
}
