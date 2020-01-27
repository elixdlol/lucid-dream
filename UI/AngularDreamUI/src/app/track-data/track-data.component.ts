import { Component, OnInit } from '@angular/core';
import { WebSocketService } from '../websocket.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-track-data',
  templateUrl: './track-data.component.html',
  styleUrls: ['./track-data.component.css']
})
export class TrackDataComponent implements OnInit {
  id = 770;
  bearing = 350.33;
  bearing_rate = 369.2;

  constructor(private webSocketService: WebSocketService, private messageService: MessageService) { }

  ngOnInit() {
    this.webSocketService.listen('client_trackData').subscribe((data) => {
      // console.log(data);
      var trackDataObj = JSON.parse(data as string);
      console.log(data);
      console.log(trackDataObj.systemTracks[0].trackID);
      console.log(trackDataObj.course_overe_ground);

      // Get the id that was clicked
      this.messageService.getMessage().subscribe(msg => {this.id = msg.text});

      // Check which id was clicked and show the data according to the id 
      trackDataObj.systemTracks.forEach(trackData => {
        if (trackData.trackID == this.id) {
          this.id = trackData.trackID;
          this.bearing_rate = trackData.relativeBearingRate;
          this.bearing = trackData.relativeBearing;
        }
      });
    });
  }

}
