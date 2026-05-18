import { Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';


@Component({
  selector: 'app-eventos',
  standalone: true,
  imports: [
    TituloComponent,
    RouterOutlet],
  templateUrl: './eventos.html',
  styleUrls: ['./eventos.scss']
})
export class EventosComponent implements OnInit {
  ngOnInit(): void {
    
  }
    
}