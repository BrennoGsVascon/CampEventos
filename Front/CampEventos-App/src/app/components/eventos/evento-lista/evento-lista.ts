import { ChangeDetectorRef, OnInit, Component, TemplateRef } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef } from 'ngx-bootstrap/modal';

import { Subject, debounceTime } from 'rxjs';

import { Evento } from '../../../models/Evento';
import { EventoService } from '../../../services/evento.service';
import { DateTimeFormatPipe } from '../../../helpers/DateTimeFormat.pipe';
import { API_CONFIG } from '../../../core/config/api.config';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { Pagination } from '../../../models/Pagination';




@Component({
  selector: 'app-evento-lista',
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    DateTimeFormatPipe,
    RouterLink,
  ],
  templateUrl: './evento-lista.html',
  styleUrl: './evento-lista.scss',
})
export class EventoListaComponent implements OnInit {
  
  
  public eventos: Evento [] = [];
  public eventoId = 0;

  public pagination = {} as Pagination;
  public pageNumber = 1;
  public pageSize = 3;
  
  public larguraImagem: number = 140;
  public margemImagem: number = 1;
  public exibirImagem: boolean = true;

  private filtroListado: string = '';
  public termoBuscaChanged: Subject<string> = new Subject<string>();
  
  public get filtroLista(): string {
    return this.filtroListado;
  }
  
  public set filtroLista(value: string) {
    this.filtroListado = value;
    this.termoBuscaChanged.next(value);
  }
  
  constructor(
    private spinner: NgxSpinnerService,
    private eventoService: EventoService,
    private toastr: ToastrService,
    private modalService: NgbModal,
    private cd: ChangeDetectorRef,
    private router: Router
  ){ }
  
  ngOnInit(): void {
    this.carregarEventos();
    this.filtrarEventos();
  }
  
  public carregarEventos(): void {
    this.spinner.show();

    this.eventoService.getEventos(this.pageNumber, this.pageSize, this.filtroLista).subscribe({
      
      next: (response) => {
        
        this.eventos = response.result ?? [];
        this.pagination = response.pagination ?? ({} as Pagination);
        this.cd.detectChanges();
      },
      
      error: (error) => {
        console.error(error);
        this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
        this.spinner.hide();
      },
      complete:() => this.spinner.hide()  
    });
  }
  public filtrarEventos(): void {
    this.termoBuscaChanged.pipe(debounceTime(1000)).subscribe((filtrarPor: string) => {
      this.pageNumber = 1;
      this.spinner.show();

      this.eventoService.getEventos(this.pageNumber, this.pageSize, filtrarPor).subscribe({
        next: (response) => {
          this.eventos = response.result ?? [];
          this.pagination = response.pagination ?? ({} as Pagination);
          this.cd.detectChanges();
        },
        error: (error) => {
          console.error(error);
          this.toastr.error('Error ao carregar os eventos', 'Error!');
          this.spinner.hide();
        },
        complete: () => this.spinner.hide(),
      })
    })
  }

  public pageChanged(event: any): void {
    this.pageNumber = event.page;
    this.carregarEventos();
  }

  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }
  
  public getImagemEvento(imagemURL?: string): string {
    if (!imagemURL || imagemURL.trim() === '') {
      return 'assets/img/semImagem.jpeg';
    }
    
    return `${API_CONFIG.imageUrl}/${imagemURL}`;
  }
  
  public detalheEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }
  
  public eventoIdSelecionado?: number;
  
  public openModal(event: any, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();

    this.eventoId = eventoId;
    this.eventoIdSelecionado = eventoId;
    
    const modalRef = this.modalService.open(template, {
      centered: true,
      backdrop: 'static',
      keyboard: false
    });
    
    modalRef.result.then(
      (result) => {
        if (result === 'confirm') {
          this.deletarEvento();
        }
      },
      () => {
        this.toastr.info('Excluir evento foi cancelado.', 'Cancelado');
        console.log('Cancelou exclusão');
      }
    );
  }
  
  public deletarEvento(): void {
    if (!this.eventoIdSelecionado) {
      this.toastr.error('Nenhum evento selecionado para exclusão.', 'Erro');
      return;
    }
    
    this.spinner.show();
    
    this.eventoService.deleteEvento(this.eventoIdSelecionado).subscribe({
      next: () => {
        this.toastr.success('O evento foi deletado com sucesso.', 'Deletado!');
        this.carregarEventos();
        
      },
      error: (error) => {
        console.error(error);
        this.toastr.error(
          `Erro ao tentar deletar o evento ${this.eventoIdSelecionado}`,
          'Erro'
        );
      },
      complete: () => this.spinner.hide(),
      
    });
  }
}
