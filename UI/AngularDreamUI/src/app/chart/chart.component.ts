import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  constructor() { }

  chartOptions = {
    responsive: true
  };
  chartData = [
    { data: [50, 90, 30, 55, 66, 88, 99, 100], label: 'Baruch' }
  ];

  chartLabels = ['0', '10', '20', '30', '40', '50', '60', '70'];

  onChartClick(event) {
    console.log(event);
  }

  newDataPoint(dataArr = [100, 100, 100], label) {

    this.chartData.forEach((dataset, index) => {
      this.chartData[index] = Object.assign({}, this.chartData[index], {
        data: [...this.chartData[index].data, dataArr[index]]
      });
    });
  
    this.chartLabels = [...this.chartLabels, label];
  
  }

  ngOnInit() {
  }

}
