import { Component, OnInit } from '@angular/core';
import { Track } from '../track';
import { WebSocketService } from '../websocket.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css']
})
export class TrackListComponent implements OnInit {

  tracks: Track[] = [

  ];

  selectedTrack: Track;
  flag = false;

  onSelect(track: Track): void {
    console.log(track.id);
    // Send the clicked track id to the service in order to present the chosen track data
    this.service.sendMessage(track.id.toString());
    
    // Emit the track id to the server in order to change the track audio to the chosen one
    this.webSocketService.emit('server_chosenTrackId', track.id.toString());
  }

  constructor(private webSocketService: WebSocketService, private service: MessageService) { }

  ngOnInit() {
    this.webSocketService.listen('client_trackData').subscribe((data) => {
      // console.log(data);
      var trackListObj = JSON.parse(data as string);
      console.log(data);
      console.log(trackListObj);
      console.log(trackListObj.systemTracks[0].trackID);



      // Set the track list that has given from server 
      
      if (this.tracks.length == 0) {
        trackListObj.systemTracks.forEach(track => {
          this.tracks.push({ id: track.trackID, name: track.trackID });
        });
      }
      else {
        //this.course = ownboatObj.course;
        trackListObj.systemTracks.forEach(track => {
          for (var i = 0; i < this.tracks.length; i++) {
            if (this.tracks[i].id == (track.trackID)) {
              this.flag = true;
            }
          }
          if (!this.flag) {
            this.tracks.push({ id: track.trackID, name: track.trackID });
            this.flag = false;
          }
        });
      }
    });
  }
}
