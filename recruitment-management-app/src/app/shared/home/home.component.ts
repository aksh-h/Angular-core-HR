import { Component, OnInit } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Color, Label, SingleDataSet } from 'ng2-charts';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private service: SharedService) {
    
  }

 FinishedInterviews= [];
 PendingInterviews = [];
 InterviewCount = [];
 WeeklyCount = [];
 MonthlyCount = [];
 DailyCount =[];

 PendingRequisitionCount =[];
 ApprovedRequistionCount =[];
 CompletedRequisitionCount=[];

 PendingPositions =[];
 CompletedPositions = [];

 
 //--Bar Graph(Interview Count)--

  barChartOptions: ChartOptions = {
    responsive: true,
  };
  barChartLabels: Label[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  //barChartLabels: Label[] = ['Completed','Pending'];

  barChartType: ChartType = 'bar';
  barChartLegend = true;
  barChartPlugins = [];
  barChartData: ChartDataSets[] = [
     { data: this.FinishedInterviews, label: 'Finished Interviews'},
     { data: this.PendingInterviews, label: 'Pending Interviews' }
    // {data: [0, 20 ],label:'PendingPositions'},
    // {data: [  55 ], label:'CompletedPositions'}
  ];

  barChartColors: Color[] = [
    { backgroundColor: 'green' },
    { backgroundColor: 'red' },
    
  ]

  //   --Pie Chart(Requisition)-- 

  public pieChartOptions: ChartOptions = {
    responsive: true,
  };
  
  public pieChartLabels: Label[] = ['Pending','Inprogess', 'Completed'];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [];
  public pieChartData: SingleDataSet = [50,35,18,20];
  //  public pieChartData: SingleDataSet = [
  //    this.PendingRequisitionCount,this.ApprovedRequistionCount,this.CompletedRequisitionCount
  //  ];

  //   --Bubble Chart (Client/Internal Interview) --

  public bubbleChartOptions: ChartOptions = {
    responsive: true,
    scales: {
      xAxes: [{
        ticks: {
          min: 0,
          max: 30,
        }
      }],
      yAxes: [{
        ticks: {
          min: 0,
          max: 30,
        }
      }]
    }
  };
  public bubbleChartType: ChartType = 'bubble';
  public bubbleChartLegend = true;

  public bubbleChartData: ChartDataSets[] = [
    {
      data: [
        { x: 10, y: 10, r: 10 },
        { x: 15, y: 5, r: 15 },
        { x: 26, y: 12, r: 23 },
        { x: 7, y: 8, r: 8 },
      ],
      label: 'Series A',
    },
  ];


  public doughnutChartLabels = ['Kavitha', 'Urmila','Ramesh'];
  public doughnutChartData = [300, 150, 220];
  public doughnutChartType = 'doughnut';


  
 

  ngOnInit(): void {


    this.service.GetAll("GetCompletedInterviewCount").subscribe((res) => {
      
      var finishedInterview  = res.FinishedInterviews;
      //this.barChartLabels =res.InterviewDate;
      var pendingInterview  = res.PendingInterviews;
      for (var i = 0; finishedInterview.length > i; i++)
      {
        this.FinishedInterviews.push(finishedInterview[i]);
      }
      for (var i = 0; pendingInterview.length > i; i++) 
      {
        this.PendingInterviews.push(pendingInterview[i]);
      }
      
      
    });

    this.service.GetAll("InterviewCount").subscribe((res)=>{
        this.InterviewCount = res.InterviewCount;
        this.WeeklyCount =res.WeeklyCount;
        this.MonthlyCount = res.MonthlyCount;
        this.DailyCount = res.DailyCount;
      
    });

    this.service.GetAll("RequisitionCount").subscribe((res)=>{
      this.PendingRequisitionCount = res.PendingRequisitionCount;
      this.ApprovedRequistionCount =res.ApprovedRequistionCount;
      this.CompletedRequisitionCount = res.CompletedRequisitionCount;
      
    
    });
  }

}
