import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { EmployeeComponent } from './employee/employee.component';
import { ApprovedComponent } from './admin/approved/approved.component';
import { DeclinedComponent } from './admin/declined/declined.component';
import { PendingComponent } from './admin/pending/pending.component';
import { GraphsComponent } from './graphs/graphs.component';

const routes: Routes = [
  {
    component: SignupComponent,
    path: "Signup"
  },
  {
    component: LoginComponent,
    path: "Login"
  },
  {
    component: EmployeeComponent,
    path: "Employee"
  },
  {
    component: PendingComponent,
    path: "Pending"
  },
  {
    component: ApprovedComponent,
    path: "Approved"
  },
  {
    component: DeclinedComponent,
    path: "Declined"
  },
  {
    component: GraphsComponent,
    path: "Graphs"
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
