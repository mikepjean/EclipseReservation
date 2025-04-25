import { Component } from '@angular/core';

@Component({
  selector: 'app-eclipse-event',
  templateUrl: './eclipse-event.component.html',
  styleUrls: ['./eclipse-event.component.css']
})
export class EclipseEventComponent {
  event = {
    name: '',
    description: '',
    eventDate: '',
    visibilityLocations: ''
  };

  constructor() { }

  onSubmit(): void {
    console.log('Event Created:', this.event);
    // Add logic to send the event data to the backend
  }
}
