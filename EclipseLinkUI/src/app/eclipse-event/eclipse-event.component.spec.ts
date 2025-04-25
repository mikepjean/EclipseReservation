import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EclipseEventComponent } from './eclipse-event.component';

describe('EclipseEventComponent', () => {
  let component: EclipseEventComponent;
  let fixture: ComponentFixture<EclipseEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EclipseEventComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EclipseEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
