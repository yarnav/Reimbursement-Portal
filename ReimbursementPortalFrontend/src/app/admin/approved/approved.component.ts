import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedServiceService } from 'src/app/Shared/shared-service.service';

@Component({
  selector: 'app-approved',
  templateUrl: './approved.component.html',
  styleUrls: ['./approved.component.css']
})
export class ApprovedComponent implements OnInit {
  Reimbursements: any = [];
  AllApprovedReimbursements: any = [];

  Filter = new FormGroup({
    FilterValue: new FormControl('')
  })

  Search = new FormGroup({
    SearchEmail: new FormControl('')
  })

  constructor(private service: SharedServiceService, private router: Router) { }

  ngOnInit(): void {
    this.ApprovedReimbursementsList();
  }


  ApprovedReimbursementsList() {
    this.service.GetApprovedReimbursements().subscribe(data => {
      this.Reimbursements = data;
      this.AllApprovedReimbursements = data;
      console.warn("Approved Reimbursements List: ", this.Reimbursements);
    })
  }

  FilterMethod(FilterParameter: any) {
    if (FilterParameter.FilterValue == '') {
      this.ApprovedReimbursementsList();
      console.warn("Filtered Reimbursements", this.Reimbursements);
    }
    else {
      this.Reimbursements = this.Reimbursements.filter((x: { ReimbursementType: any; }) => x.ReimbursementType === FilterParameter.FilterValue);
      console.warn("Filtered Reimbursements", this.Reimbursements)
    }
  }

  ReimbursementReport() {
    this.router.navigate(['/Graphs'])
  }

  SearchMethod(EmailParameter: any) {
    console.warn("SearchMethod Parameter", EmailParameter)
    if (EmailParameter.SearchEmail == '') {
      this.Reimbursements = this.AllApprovedReimbursements;
      console.warn("Searched Reimbursements", this.Reimbursements);
    }
    else {
      this.Reimbursements = this.Reimbursements.filter((x: { RequestedBy: any; }) => x.RequestedBy === EmailParameter.SearchEmail);
      console.warn("Searched Reimbursements", this.Reimbursements)
    }
  }

  Logout() {
    this.service.LoggedInUserData = null
    this.router.navigate(['/Login'])
  }

}
