import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { TrackComponent } from './track/track.component';
import { TrackListComponent } from './track-list/track-list.component';
import { OwnboatComponent } from './ownboat/ownboat.component';
import { ChartComponent } from './chart/chart.component';
import { TrackDataComponent } from './track-data/track-data.component';
import { CustomTrackDataComponent } from './custom-track-data/custom-track-data.component';
import { AudioManagerComponent } from './audio-manager/audio-manager.component';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AppComponent,
    TrackComponent,
    TrackListComponent,
    OwnboatComponent,
    ChartComponent,
    TrackDataComponent,
    CustomTrackDataComponent,
    AudioManagerComponent
  ],
  imports: [
    BrowserModule,
    ChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
