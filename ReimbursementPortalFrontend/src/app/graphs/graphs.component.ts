import { SharedServiceService } from '../Shared/shared-service.service';
import { Component, OnInit } from '@angular/core';
import Chart, {  } from 'chart.js/auto';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-graphs',
  templateUrl: './graphs.component.html',
  styleUrls: ['./graphs.component.css']
})


export class GraphsComponent implements OnInit {
  MonthWiseCount: any = [];
  CategoryWiseCount: any = [];
  RandomArray: any = [];
  constructor(private service: SharedServiceService, private router: Router, private location: Location) {
  }

  Logout() {
    this.router.navigate(['/Login'])
  }
  Goback() {
    this.location.back();
  }
  RandomMethod(array: any) {
    console.warn("This is Random Method", array);
    this.MonthWiseCount = array;
  }

  ngOnInit() {

    this.service.MonthWiseData().subscribe(data => {
      this.MonthWiseCount = data;
      this.RandomMethod(data);
      console.warn("MonthWiseData() inside function", this.MonthWiseCount);
    })
    console.warn("MonthWiseData() outside", this.MonthWiseCount);

    this.service.CategoryWiseData().subscribe(data => {
      this.CategoryWiseCount = data;
      console.warn("CategoryWiseData inside function", this.CategoryWiseCount);
    })
    console.warn("CategoryWiseData Outside funtion", this.CategoryWiseCount)

    const MyChart = new Chart("MonthChart", {
      type: 'bar',
      data: {
        labels: [
          'January',
          'February',
          'March',
          'April',
          'May',
          'June',
          'July',
          'August',
          'September',
          'October',
          'November',
          'December'
        ],
        datasets: [{
          label: 'Reimbursements Count',
          //data: this.MonthWiseCount,
          data: [1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4],
          backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(255, 206, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(255, 159, 64, 0.2)',
            'rgba(255, 99, 132, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(255, 206, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(255, 159, 64, 0.2)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)',
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });

    const CategoryChart = new Chart("CategoryChart", {
      type: 'polarArea',
      data: {
        labels: [
          'Medical',
          'Food',
          'Travel',
          'Entertainment',
          'Misc.'
        ],
        datasets: [{
          label: 'Category Count',
          //data: this.CategoryWiseCount,
          data: [3, 4, 3, 3, 2],
          backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(75, 192, 192)',
            'rgb(255, 205, 86)',
            'rgb(201, 203, 207)',
            'rgb(54, 162, 235)'
          ]
        }]
      },
      options: {
      }
    });
  }
}
