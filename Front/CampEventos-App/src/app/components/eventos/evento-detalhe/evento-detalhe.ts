import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Evento } from '../../../models/Evento';
import { EventoService } from '../../../services/evento.service';

import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-evento-detalhe',
  imports: [ReactiveFormsModule, 
    CommonModule,
    BsDatepickerModule,
  ],
  
  templateUrl: './evento-detalhe.html',
  styleUrl: './evento-detalhe.scss',
})
export class EventoDetalheComponent implements OnInit {
  
  evento = {} as Evento;
  form!: FormGroup;
  estadoSalvar: 'post' | 'put' = 'post';
  
  get f(): any {
    return this.form.controls;
  }
  
  bsConfig = {
    adaptivePosition: true,
    dateInputFormat: 'DD/MM/YYYY hh:mm',
    containerClass: 'theme-default',
    showWeekNumbers: false
  };
  
  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService )
    
    {
      this.localeService.use('pt-br')
    }
    
    public carregarEvento(): void {
      const eventoIdParam = this.router.snapshot.paramMap.get('id');
      
      
      if (eventoIdParam !== null) {
        this.spinner.show();
        
        this.estadoSalvar = 'put';
        
        this.eventoService.getEventoById(+eventoIdParam).subscribe({
          next: (evento: Evento) => {
            this.evento = { ...evento };
            this.form.patchValue(this.evento);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar Evento', 'Erro!')
            console.error(error);
          },
          complete: () => this.spinner.hide(),
        });
      }
    }
    
    ngOnInit(): void {
      this.validation();
      this.carregarEvento();
    }
    
    public validation(): void {
      this.form = this.fb.group({
        
        tema : ['', [Validators.required,Validators.minLength(4),Validators.maxLength(50),]],
        local : ['', Validators.required],
        dataEvento: ['', Validators.required],
        qtdPessoas : ['', [Validators.required,Validators.max(10000),]],
        modalidade : ['', Validators.required],
        telefone : ['', Validators.required],
        email: ['', [ Validators.required, Validators.email]],
        imagemURL: ['', Validators.required],
        descricao: ['', Validators.required],
        
      });
    }
    
    public resetForm(): void {
      this.form.reset();
    }
    
    public cssValidator(campoForm: string): any {
      return {
        'is-invalid': this.f[campoForm]?.errors && this.f[campoForm]?.touched,
        'is-valid': this.f[campoForm]?.valid && this.f[campoForm]?.touched
      };
    }
    
    public salvarAlteracao(): void {
      if (this.form.invalid) {
        this.form.markAllAsTouched();
        this.toastr.warning('Preencha os campos obrigatórios.', 'Atenção');
        return;
      }
      
      this.spinner.show();
      
      this.evento = this.estadoSalvar === 'post'
      ? { ...this.form.value }
      : { id: this.evento.id, ...this.form.value };
      
      this.eventoService[this.estadoSalvar](this.evento).subscribe({
        next: () => {
         this.toastr.success(
          this.estadoSalvar === 'post'
              ? 'Evento cadastrado com sucesso!'
              : 'Evento atualizado com sucesso!','Sucesso');
        },
        error: (error: any) => {
          console.error(error);
           this.spinner.hide();
          this.toastr.error('Erro ao salvar Evento', 'Erro!');
        },
        complete: () => {
          this.spinner.hide();
        }
      });
    }
  }