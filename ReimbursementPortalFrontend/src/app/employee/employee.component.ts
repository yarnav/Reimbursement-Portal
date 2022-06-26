import { Component, OnInit } from '@angular/core';
import { SharedServiceService } from 'src/app/Shared/shared-service.service';
import { FormsModule, ReactiveFormsModule, FormControlDirective, FormGroup, FormControl, Validators } from '@angular/forms';
import { ThisReceiver } from '@angular/compiler';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  Reimbursements: any = [];
  UpdateReimbursementID: any = [];
  DetailsToBePrefilled: any = [];
  alert = false;

  AddReimbursement = new FormGroup({
    Date: new FormControl('', Validators.required),
    ReimbursementType: new FormControl('', Validators.required),
    RequestedValue: new FormControl('', Validators.required),
    Currency: new FormControl('', Validators.required),
    ImageURL: new FormControl(''),
    EmployeeID: new FormControl(''),
    RequestedBy: new FormControl('')
  });
  UpdateReimbursement = new FormGroup({
    Date: new FormControl('', Validators.required),
    ReimbursementID: new FormControl(''),
    ReimbursementType: new FormControl('', Validators.required),
    RequestedValue: new FormControl('', Validators.required),
    Currency: new FormControl('', Validators.required),
    ImageURL: new FormControl('')
  });

  constructor(private service: SharedServiceService, private router: Router) {
  }

  ngOnInit(): void {
    this.service.GetReimbursementsByEmployeeID().subscribe(data => {
      console.warn("GetR BY ID", data);
    });
    this.ReimbursementsList();
    this.ShowMyReimbursements();
  }

  ShowMyReimbursements() {
    this.Reimbursements = this.Reimbursements.filter((x: { EmployeeID: any; }) => x.EmployeeID === this.service.LoggedInUserData.EmployeeID);
  }

  Update(rid: any) {
    this.UpdateReimbursementID = rid;
    this.DetailsToBePrefilled = this.Reimbursements.filter((x: { ReimbursementID: any; }) => x.ReimbursementID == this.UpdateReimbursementID)
    console.warn("Details to be filled ", this.DetailsToBePrefilled);
    this.UpdateReimbursement.patchValue({
      Date: this.DetailsToBePrefilled['Date'],
      ReimbursementID: this.DetailsToBePrefilled['ReimbursementID'],
      ReimbursementType: this.DetailsToBePrefilled['ReimbursementType'],
      RequestedValue: this.DetailsToBePrefilled['RequestedValue'],
      Currency: this.DetailsToBePrefilled['Currency'],
      ImageURL: this.DetailsToBePrefilled['ImageURL']
    })
    console.warn("Update Details ", this.UpdateReimbursement);
  }

  ReimbursementsList() {
    this.service.GetReimbursementsByEmployeeID().subscribe(data => {
      console.warn("Reimbursements List: ", data);
      this.Reimbursements = data;
    })
  }

  AddingReimbursement(reimbursement: any) {
    this.AddReimbursement.value.EmployeeID = this.service.LoggedInUserData.EmployeeID;
    this.AddReimbursement.value.RequestedBy = this.service.LoggedInUserData.Email;
    this.service.PostReimbursement(reimbursement).subscribe(result => {
      console.warn("Posted Reimbursement returning: ", result);
      this.RefreshList();
    });
    this.AddReimbursement.reset({});
    console.warn("AddReimbursement Object", this.AddReimbursement.value);
  }

  GetFormValues(data: any) {
    console.warn("Values from form: ", data);
  }
  CloseAlert() {
    this.alert = false;
  }

  UpdatingReimbursement(updatedValues: any) {
    this.UpdateReimbursement.value.ReimbursementID = this.UpdateReimbursementID;
    console.warn("Update ID Recieved", this.UpdateReimbursementID, "Updated Form Values", updatedValues);
    this.service.UpdateReimbursement(updatedValues).subscribe(result => {
      if (result) {
        console.warn("Result updated");
        this.RefreshList();
        this.UpdateReimbursement.reset({});
      }
      else {
        this.alert = true;
        console.warn("Some Error Occured Update can't be performed");
      }
    })
  }

  DeletingReimbursement(id: any) {
    this.service.DeleteReimbursement(id).subscribe(result => {
      console.warn("Delete Reimbursement with ID", id)
      this.RefreshList();
    });

  }

  RefreshList() {
    this.service.GetReimbursementsByEmployeeID().subscribe(reimbursements => {
      this.Reimbursements = reimbursements;
    })
    console.warn("List Refreshed");
  }

  ReimbursementReport() {
    this.router.navigate(['/Graphs']);
  }

  Logout() {
    this.service.LoggedInUserData = null;
    this.router.navigate(['/Login']);
  }

  get Date() {
    return this.AddReimbursement.get('Date');
  }
  get RequestedValue() {
    return this.AddReimbursement.get('RequestedValue');
  }
  get Currency() {
    return this.AddReimbursement.get('Currency');
  }
  get ReimbursementType() {
    return this.AddReimbursement.get('ReimbursementType');
  }

  get UpdateDate() {
    return this.UpdateReimbursement.get('Date');
  }
  get UpdateRequestedValue() {
    return this.UpdateReimbursement.get('RequestedValue');
  }
  get UpdateCurrency() {
    return this.UpdateReimbursement.get('Currency');
  }
  get UpdateReimbursementType() {
    return this.UpdateReimbursement.get('ReimbursementType');
  }
}
