import { Component, OnInit, ChangeDetectorRef, TemplateRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { TituloComponent } from '../../shared/titulo/titulo.component';
import { EventoService } from '../../services/evento.service';
import { Evento } from '../../models/Evento';
import { DateTimeFormatPipe } from '../../helpers/DateTimeFormat.pipe';


@Component({
  selector: 'app-eventos',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    DateTimeFormatPipe,
    TituloComponent
],
  templateUrl: './eventos.html',
  styleUrls: ['./eventos.scss'],
})
export class EventosComponent implements OnInit {
  
    constructor(
    private spinner: NgxSpinnerService,
    private eventoService: EventoService,
    private toastr: ToastrService,
    private modalService: NgbModal,
    private cd: ChangeDetectorRef
  ){}
    
    
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
  
    public ngOnInit(): void {
      this.spinner.show();
      this.getEventos();
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
          complete:() => this.spinner.hide()  
        });
    }
}