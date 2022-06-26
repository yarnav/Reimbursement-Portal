import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ReimbursementPortalFrontend'
  StudentLogin = false

  constructor(private router: Router) { }

  GetFormValues(value: any) {
    console.warn(value)
  }
  ngOnInit(): void {
    this.router.navigate(['/Login'])
  }
}
