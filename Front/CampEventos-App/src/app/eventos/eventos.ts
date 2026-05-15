import { Component, OnInit, ChangeDetectorRef, TemplateRef, inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';
import { DateTimeFormatPipe } from '../helpers/DateTimeFormat.pipe';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-eventos',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    DateTimeFormatPipe,
  ],
  templateUrl: './eventos.html',
  styleUrls: ['./eventos.scss'],
})
export class EventosComponent implements OnInit {
  private spinner = inject(NgxSpinnerService);
  private eventoService = inject(EventoService);
  private toastr = inject(ToastrService);
  private modalService = inject(NgbModal);
  private cd = inject(ChangeDetectorRef);

  public eventoIdSelecionado?: number;

  public openModal(template: TemplateRef<any>, eventoId: number): void {
    this.eventoIdSelecionado = eventoId;

  const modalRef = this.modalService.open(template, {
      centered: true,
      backdrop: 'static',
      keyboard: false
    });

  modalRef.result.then(
    (result) => {
      if (result === 'confirm') {
        this.toastr.success('Evento excluído com sucesso!', 'Sucesso');
        console.log('Confirmou exclusão do evento:', this.eventoIdSelecionado);
      }
    },
    () => {
      this.toastr.info('Exclusão cancelada.', 'Cancelado');
      console.log('Cancelou exclusão');
    }
  );
}

  public eventos: Evento [] = [];
  public eventosFiltrados: Evento [] = [];

  public larguraImagem: number = 140;
  public margemImagem: number = 1;
  public exibirImagem: boolean = true;
  private filtroListado: string = '';

  public get filtroLista(): string {
    return this.filtroListado;
  }

  public set filtroLista(value: string) {
    this.filtroListado = value;

    this.eventosFiltrados = this.filtroListado
    ? this.filtrarEventos(this.filtroListado) 
    : this.eventos;
  }

  public filtrarEventos(filtrarPor :string): Evento[] { 
    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().includes(filtrarPor) ||
      evento.local.toLocaleLowerCase().includes(filtrarPor) 
    );
  }

  constructor() { }

  public ngOnInit(): void {
    this.spinner.show();

  this.getEventos();

  setTimeout(() => {
    this.spinner.hide();
  }, 2000);

  }
  
  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({

        next: (eventosResp: Evento[]) => {

          console.log('RETORNO DA API:', eventosResp);

          this.eventos = eventosResp;
          this.eventosFiltrados = this.eventos;
          this.cd.detectChanges();
        },

        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao Carregar os Eventos', 'Erro!')
        },
        //complete:() => this.spinner.hide()  
      });
  }
}