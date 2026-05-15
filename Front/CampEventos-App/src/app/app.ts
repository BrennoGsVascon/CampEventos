import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./shared/nav/nav.component";

import { NgxSpinnerModule } from 'ngx-spinner';


@Component({
    selector: 'app-root',
    standalone: true ,
    imports: [
    NavComponent,
    RouterOutlet,
    NgxSpinnerModule,   
],
    templateUrl: './app.html',
    styleUrls: ['./app.scss'],
    
  })
export class App {
  protected readonly title = signal('CampEventos-App');
}
