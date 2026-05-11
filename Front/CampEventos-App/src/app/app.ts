import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NavComponent} from "./nav/nav.component";



@Component({
  selector: 'app-root',
  standalone: true ,
  imports: [NavComponent, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('CampEventos-App');
}
