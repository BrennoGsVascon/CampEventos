import { Component } from '@angular/core';
import { TituloComponent } from '../../shared/titulo/titulo.component';


@Component({
  selector: 'app-apresentadores',
  standalone: true,
  imports: [TituloComponent],
  templateUrl: './apresentadores.html',
  styleUrl: './apresentadores.scss',
})
export class ApresentadoresComponent {}
