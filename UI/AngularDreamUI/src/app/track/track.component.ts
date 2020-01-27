import { Component, OnInit } from '@angular/core';
import { Track } from '../Track';

@Component({
  selector: 'app-track',
  templateUrl: './track.component.html',
  styleUrls: ['./track.component.css']
})
export class TrackComponent implements OnInit {

  track: Track;

  constructor() { }

  ngOnInit() {
  }

}
