import { Component, OnInit, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { WebSocketService } from ".././websocket.service";

@Component({
  selector: 'app-audio-manager',
  templateUrl: './audio-manager.component.html',
  styleUrls: ['./audio-manager.component.css']
})
export class AudioManagerComponent implements OnInit, AfterViewInit {

  @ViewChild('seekslider', { static: false }) seeksliderRef: ElementRef;

  // Properties of the class
  hidePlayButton: boolean;
  startRecordingButtonEnabled: boolean;
  myAudio: HTMLAudioElement;
  playRecordInteravlId: any;
  playRecordingButtonEnabled: boolean;
  blockSeek: boolean;
  audioWasPaused: boolean;
  sliderCurrentValue: string;

  constructor(private webSocketService: WebSocketService) {
    this.hidePlayButton = false;
    this.startRecordingButtonEnabled = true;
    this.playRecordingButtonEnabled = false;
    this.blockSeek = false;
    this.audioWasPaused = true;
    this.sliderCurrentValue = "0";
    this.myAudio = new Audio();
  }

  getAudioSourceFromServer() {
    this.myAudio.src = 'http://localhost:3000/wav';
  };

  // Create event listeners for the audio
  initAudioPlayer() {

    // Listener for event: 'ended'
    this.myAudio.addEventListener('ended', () => {
      this.hidePlayButton = false;
      this.seeksliderRef.nativeElement.value = 0;
      clearInterval(this.playRecordInteravlId);
    });

    // Listener for event: 'play'
    this.myAudio.addEventListener('play', () => {
      this.hidePlayButton = true;
    });

    // Listener for event: 'pause'
    this.myAudio.addEventListener('pause', () => {
      this.hidePlayButton = false;
    });

    // Listener for event: 'loadedmetadata'
    this.myAudio.addEventListener('loadedmetadata', () => {
      console.log("wav duration: " + this.myAudio.duration);
      this.seeksliderRef.nativeElement.max = this.myAudio.duration;

      // Button control
      this.playRecordingButtonEnabled = true;
    });
  };

  playRecordedAudio() {
    this.myAudio.play();
  };

  pauseRecordedAudio() {
    this.myAudio.pause();
  };

  startRecording() {
    // Button control
    this.startRecordingButtonEnabled = false;

    // Emit signal to server
    this.webSocketService.emit('server_startRecording', 'Record');
  };

  stopRecording() {
    // Button control
    this.startRecordingButtonEnabled = true;

    // Emit signal to server
    this.webSocketService.emit('server_stopRecording', 'Stop');

    // get the audio we recorded
    this.getAudioSourceFromServer();
  };

  
  onSeek = (event) => {
    console.log("onSeek val is :" + event.target.value );
    if (!this.blockSeek) {
      this.blockSeek = true;
     this.audioWasPaused = this.myAudio.paused;
     this.myAudio.pause();
    }
    console.log("onSeek: slider seeked value: " + this.sliderCurrentValue );
    console.log("onSeek: currentTime-1: " + this.myAudio.currentTime );
    this.myAudio.currentTime = 1;
   console.log("onSeek: currentTime-2: " + this.myAudio.currentTime );
  }

  onSeekRelease = () => {
    console.log("onSeekRelease");
    console.log("onSeekRelease: slider seeked value: " + this.sliderCurrentValue );
    console.log("onSeekRelease: slider current time: " + this.myAudio.currentTime );
    if (!this.audioWasPaused) {
      this.myAudio.play();
    }
    this.blockSeek = false;
  }

  onTimeupdate = () => {
    console.log("onTimeupdate");
    if (!this.blockSeek) {
      this.sliderCurrentValue = this.myAudio.currentTime.toString();
    }
  }

  ngOnInit() {
    // Listen to events from server
    this.webSocketService.listen('client_recordingStarted').subscribe((data) => {
      console.log(data);
    });

    this.webSocketService.listen('client_recordingStopped').subscribe((data) => {
      console.log(data);
    });

  }

  ngAfterViewInit() {

    this.myAudio.addEventListener('timeupdate', this.onTimeupdate, false);
    this.seeksliderRef.nativeElement.addEventListener('input', this.onSeek, false);
    this.seeksliderRef.nativeElement.addEventListener('change', this.onSeekRelease, false);

    this.initAudioPlayer();
  }
}
