import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomTrackDataComponent } from './custom-track-data.component';

describe('CustomTrackDataComponent', () => {
  let component: CustomTrackDataComponent;
  let fixture: ComponentFixture<CustomTrackDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomTrackDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomTrackDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
