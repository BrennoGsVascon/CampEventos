import { ChangeDetectorRef, OnInit, Component, TemplateRef } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '../../../models/Evento';
import { EventoService } from '../../../services/evento.service';
import { DateTimeFormatPipe } from '../../../helpers/DateTimeFormat.pipe';




@Component({
  selector: 'app-evento-lista',
  imports: [
    CommonModule,
    FormsModule,
    DateTimeFormatPipe,
    RouterLink,
],
  templateUrl: './evento-lista.html',
  styleUrl: './evento-lista.scss',
})
export class EventoListaComponent implements OnInit {
  
  constructor(
    private spinner: NgxSpinnerService,
    private eventoService: EventoService,
    private toastr: ToastrService,
    private modalService: NgbModal,
    private cd: ChangeDetectorRef,
    private router: Router
  ){}
  
  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }
  
  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }
  
  public detalheEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }
  
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

      eventos: Evento [] = [];
      eventosFiltrados: Evento [] = [];

      larguraImagem: number = 140;
      margemImagem: number = 1;
      exibirImagem: boolean = true;
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
          complete:() => this.spinner.hide()  
        });
    }
}
