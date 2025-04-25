import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule for template-driven forms }

import { AppComponent } from './app.component';
import { EclipseEventComponent } from './eclipse-event/eclipse-event.component';

@NgModule({
  declarations: [
    AppComponent,
    EclipseEventComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
