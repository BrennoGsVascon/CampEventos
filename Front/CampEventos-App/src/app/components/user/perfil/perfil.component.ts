import { Component, OnInit } from '@angular/core';
import { TituloComponent } from '../../../shared/titulo/titulo.component';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [TituloComponent],
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
