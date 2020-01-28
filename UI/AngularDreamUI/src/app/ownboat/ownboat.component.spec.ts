import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnboatComponent } from './ownboat.component';

describe('OwnboatComponent', () => {
  let component: OwnboatComponent;
  let fixture: ComponentFixture<OwnboatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OwnboatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OwnboatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
