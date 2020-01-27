import { Component, OnInit } from '@angular/core';
import { WebSocketService } from ".././websocket.service";

@Component({
  selector: 'app-ownboat',
  templateUrl: './ownboat.component.html',
  styleUrls: ['./ownboat.component.css']
})
export class OwnboatComponent implements OnInit {

  //course = 228.770;
  roll = 228.770;
  pitch = 228.770;
  //propeller_rpm = 228.770;
  //diving_depth = 228.770;
  course_overe_ground = 0;
  heave = 0;
  heading = 0;

  ownboatTime='';
  constructor(private webSocketService: WebSocketService) { }

  ngOnInit() {

    this.webSocketService.listen('client_ownboatData').subscribe((data) => {
      // console.log(data);
      var ownboatObj = JSON.parse(data as string);
      console.log(data);
      console.log(ownboatObj);
      console.log(ownboatObj.course_overe_ground);

      // Get all ownboat data from server and present it 
      this.roll = ownboatObj.roll;
      this.pitch = ownboatObj.pitch;

      this.course_overe_ground = ownboatObj.course_overe_ground;
      this.heave = ownboatObj.heave;
      this.heading = ownboatObj.heading;

      this.ownboatTime = ownboatObj.timeStamp.day + '\\' + ownboatObj.timeStamp.month + '\\' + ownboatObj.timeStamp.year
        + ' :' + ownboatObj.timeStamp.hours + ':' + ownboatObj.timeStamp.minutes + ':' + ownboatObj.timeStamp.seconds;
    });
  }

}
