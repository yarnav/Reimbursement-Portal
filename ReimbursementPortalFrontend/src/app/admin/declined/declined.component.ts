import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedServiceService } from 'src/app/Shared/shared-service.service';

@Component({
  selector: 'app-declined',
  templateUrl: './declined.component.html',
  styleUrls: ['./declined.component.css']
})
export class DeclinedComponent implements OnInit {
  Reimbursements: any = [];
  AllReimbursements: any = [];

  Filter = new FormGroup({
    FilterValue: new FormControl('')
  })

  Search = new FormGroup({
    SearchEmail: new FormControl('')
  })

  constructor(private service: SharedServiceService, private router: Router) { }

  ngOnInit(): void {
    this.ReimbursementsList();
  }

  ReimbursementsList() {
    this.service.GetDeclinedReimbursements().subscribe(data => {
      console.warn("Declined Reimbursements List: ", data);
      this.Reimbursements = data;
      this.AllReimbursements = data;
    })
  }

  FilterMethod(FilterParameter: any) {
    if (FilterParameter.FilterValue == '') {
      this.Reimbursements = this.AllReimbursements;
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
      this.Reimbursements = this.AllReimbursements;
      console.warn("Searched Reimbursements", this.Reimbursements)
    }
    else {
      this.Reimbursements = this.Reimbursements.filter((x: { RequestedBy: any; }) => x.RequestedBy === EmailParameter.SearchEmail);
      console.warn("Searched Reimbursements", this.Reimbursements)
    }
  }

  ReimbursementReport() {
    this.router.navigate(['/Graphs'])
  }

  Logout() {
    this.service.LoggedInUserData = null
    this.router.navigate(['/Login'])
  }

}
