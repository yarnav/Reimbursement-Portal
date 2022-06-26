import { Component, OnInit } from '@angular/core';
import { SharedServiceService } from 'src/app/Shared/shared-service.service';
import { FormsModule, ReactiveFormsModule, FormControlDirective, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pending',
  templateUrl: './pending.component.html'
})
export class PendingComponent implements OnInit {
  Filter = new FormGroup({
    FilterValue: new FormControl('')
  })

  Search = new FormGroup({
    SearchEmail: new FormControl('')
  })

  Approval = new FormGroup({
    ApprovedBy: new FormControl('', Validators.required),
    ApprovedValue: new FormControl('', Validators.required),
    InternalNotes: new FormControl('', Validators.required)
  })

  Reimbursements: any = [];
  AllPendingReimbursements: any = [];
  ReimbursementIDForApprovalObject: any = [];
  ObjectForApproval: any = [];

  constructor(private service: SharedServiceService, private router: Router) { }

  ngOnInit(): void {
    this.PeningReimbursementsList();
  }

  FilterMethod(FilterParameter: any) {
    if (FilterParameter.FilterValue == '') {
      this.Reimbursements = this.AllPendingReimbursements;
      console.warn("Filtered Reimbursements", this.Reimbursements)
    }
    else {
      this.Reimbursements = this.Reimbursements.filter((x: { ReimbursementType: any; }) => x.ReimbursementType === FilterParameter.FilterValue);
      console.warn("Filtered Reimbursements", this.Reimbursements)
    }
  }

  SearchMethod(EmailParameter: any) {
    console.warn("SearchMethod Parameter", EmailParameter)
    if (EmailParameter.SearchEmail == '') {
      this.Reimbursements = this.AllPendingReimbursements;
      console.warn("Searched Reimbursements", this.Reimbursements)
    }
    else {
      this.Reimbursements = this.Reimbursements.filter((x: { RequestedBy: any; }) => x.RequestedBy === EmailParameter.SearchEmail);
      console.warn("Searched Reimbursements", this.Reimbursements)
    }
  }

  PeningReimbursementsList() {
    this.service.GetPendingReimbursements().subscribe(data => {
      console.warn("Pending Reimbursements List: ", data)
      this.Reimbursements = data;
      this.AllPendingReimbursements = data;
      console.warn("All Reimbursements ", this.AllPendingReimbursements)
    })
  }

  RefreshList() {
    this.service.GetPendingReimbursements().subscribe(results => { this.Reimbursements = results }
    );
  }

  GetReimbursementIdOnClick(ReimbursementId: any) {
    this.ReimbursementIDForApprovalObject = ReimbursementId;
  }

  GetFormValues() {
    console.warn("Values from Admin Approval form ", this.Approval.value)
    this.ObjectForApproval = {
      ReimbursementID: this.ReimbursementIDForApprovalObject,
      ApprovedBy: this.Approval.value.ApprovedBy,
      ApprovedValue: this.Approval.value.ApprovedValue,
      InternalNotes: this.Approval.value.InternalNotes
    }
    console.warn("Objectfor", this.ObjectForApproval)
    this.ApprovingReimbursement()
  }

  ApprovingReimbursement() {
    this.service.ApproveReimbursement(this.ObjectForApproval).subscribe(result => {
      console.warn("Data coming back to ApproveReimbursement()", result)
      this.RefreshList()
    })
  }

  DecliningReimbursement(reimbursementId: any) {
    console.warn("Decline ReimbursementID", reimbursementId)
    this.service.DeclineReimbursement(reimbursementId).subscribe(result => {
      console.warn("Data coming back to Decline()", result)
      this.RefreshList()
    })
  }

  ReimbursementReport() {
    this.router.navigate(['/Graphs'])
  }

  Logout() {
    this.service.LoggedInUserData = null
    this.router.navigate(['/Login'])
  }

}
