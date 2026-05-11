import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-eventos',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './eventos.html',
  styleUrl: './eventos.scss',
})
export class EventosComponent implements OnInit {

  public eventos: any [] = [];
  public eventosFiltrados: any [] = [];

  larguraImagem: number = 140;
  margemImagem: number = 1;
  exibirImagem: boolean = true;
  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;

    this.eventosFiltrados = this._filtroLista
    ? this.filtrarEventos(this._filtroLista) 
    : this.eventos;
  }

  public filtrarEventos(filtrarPor :string): any[] { 
    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().includes(filtrarPor) ||
      evento.local.toLocaleLowerCase().includes(filtrarPor) 
    );
  }

  alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }

  constructor(
    private http: HttpClient,
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {

    this.http
      .get<any[]>('https://localhost:5001/api/Eventos')
      .subscribe({

        next: (response) => {

          console.log('RETORNO DA API:', response);

          this.eventos = response;

          this.eventosFiltrados = this.eventos;

          this.cd.detectChanges();
        },

        error: (error) => {
          console.log(error);
        }

      });
  }
}