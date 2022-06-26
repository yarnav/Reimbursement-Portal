import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {
  readonly APIUrl = "https://localhost:44353/";
  Reimbursements: any;
  LoggedInUserData: any;
  MonthsCount: any;

  constructor(private http: HttpClient) { }

  GetReimbursements(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + 'GetReimbursement')
  }
  GetReimbursementsByEmployeeID() {
    return this.http.get(`${this.APIUrl}GetReimbursementById/${this.LoggedInUserData.EmployeeID}`)
  }
  PostReimbursement(reimbursement: any) {
    return this.http.post(this.APIUrl + 'AddReimbursement', reimbursement)
  }
  DeleteReimbursement(id: any) {
    return this.http.delete(`${this.APIUrl}DeleteReimbursement/${id}`)
  }
  PostEmployee(employee: any) {
    return this.http.post(this.APIUrl + 'Signup', employee)
  }
  UpdateReimbursement(reimbursement: any) {
    return this.http.put(this.APIUrl + 'UpdateReimbursement', reimbursement)
  }
  Login(credentials: any) {
    console.warn("Credentials in Shared Component ", credentials)
    return this.http.post(this.APIUrl + `Login`, credentials)
  }
  ApproveReimbursement(reimbursement: any) {
    return this.http.put(this.APIUrl + 'ApproveReimbursement', reimbursement)
  }
  DeclineReimbursement(reimbursementId: any) {
    return this.http.put(this.APIUrl + 'DeclineReimbursement', reimbursementId)
  }
  MonthWiseData(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + 'GetMonthsCount')
  }
  CategoryWiseData() {
    return this.http.get(this.APIUrl + 'GetCategoryCount')
  }
  GetPendingReimbursements() {
    return this.http.get(this.APIUrl + 'GetPendingReimbursements');
  }
  GetApprovedReimbursements() {
    return this.http.get(this.APIUrl + 'GetApprovedReimbursements')
  }
  GetDeclinedReimbursements() {
    return this.http.get(this.APIUrl + 'GetDeclinedReimbursements')
  }


  //                                    filter is  not recognized
  // FilterService(FilterParameter:any){
  //   this.Reimbursements=this.GetReimbursements()
  //   if(FilterParameter.FilterValue=='')
  //   {
  //     console.warn("Filtered Reimbursements",this.Reimbursements)
  //     return this.GetReimbursements()
  //   }
  //   else
  //   {
  //     console.warn("Filtered Reimbursements",this.Reimbursements.filter((x: { ReimbursementType: any; })=>x.ReimbursementType===FilterParameter.FilterValue))
  //     return this.Reimbursements.filter((x: { ReimbursementType: any; })=>x.ReimbursementType===FilterParameter.FilterValue);
  //   }    
  // }
}
