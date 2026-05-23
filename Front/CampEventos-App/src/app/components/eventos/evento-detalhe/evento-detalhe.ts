import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef, ChangeDetectorRef } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Evento } from '../../../models/Evento';
import { EventoService } from '../../../services/evento.service';
import { Lote } from '../../../models/Lote';
import { LoteService } from '../../../services/Lote.service';
import { API_CONFIG } from '../../../core/config/api.config';

import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';






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
  modalRef?: BsModalRef;
  
  eventoId = 0;
  evento = {} as Evento;
  form!: FormGroup;
  estadoSalvar: 'post' | 'put' = 'post';
  lotesCarregados = false;
  loteAtual = {id: 0,nome: '', index: 0};
  imagemPreview = 'assets/img/semImagem.jpeg';
  file?: File;
  
  bsConfig = {
    adaptivePosition: true,
    dateInputFormat: 'DD/MM/YYYY hh:mm',
    containerClass: 'theme-default',
    showWeekNumbers: false
  };
  
  
  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private loteService: LoteService,
    private modalService: BsModalService,
    private cdRef: ChangeDetectorRef,
  )
  
  {
    this.localeService.use('pt-br');
  }
  
  ngOnInit(): void {
    this.validation();
    
    this.activatedRouter.paramMap.subscribe(params => {
      this.eventoId = Number(params.get('id') ?? 0);
      
      this.form.reset();
      this.lotes.clear();
      this.lotesCarregados = false;
      
      if (this.eventoId > 0) {
        this.estadoSalvar = 'put';
        this.carregarEvento();
      } else {
        this.estadoSalvar = 'post';
      }
    });
  }
  
  get modoEditar(): boolean{
    return this.estadoSalvar == 'put' && this.eventoId > 0;
  }
  
  get lotes(): FormArray{
    return this.form.get('lotes') as FormArray;
  }
  
  get lotesFormGroups(): FormGroup[] {
    return this.lotes.controls as FormGroup[];
  }
  
  get f(): any {
    return this.form.controls;
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
      imagemURL: [''],
      descricao: ['', Validators.required],
      lotes: this.fb.array([])
    });
  }
  
  public carregarEvento(): void {
    this.spinner.show();
    
    this.eventoService.getEventoById(this.eventoId).subscribe({
      next: (evento: Evento) => {
        this.evento = { ...evento };
        
        const { lotes, ...eventoSemLotes } = evento as any;
        
        this.form.patchValue(this.evento);
        this.imagemPreview = this.evento.imagemURL
        ? `${API_CONFIG.imageUrl}/${this.evento.imagemURL}`
        : 'assets/img/semImagem.jpeg';
        this.carregarLotes();
      },
      error: (error: any) => {
        console.error(error);
        this.toastr.error('Erro ao tentar carregar Evento', 'Erro!');
        this.spinner.hide();
      }
    });
  }
  
  public carregarLotes(): void {
    this.lotesCarregados = false;
    this.lotes.clear();
    
    this.loteService.getLotesByEventoId(this.eventoId).subscribe({
      next: (lotesRetorno: Lote[] | null) => {
        (lotesRetorno ?? []).forEach((lote: Lote) => {
          this.lotes.push(this.criarLote(lote));
        });
        
        this.lotesCarregados = true;
        this.cdRef.detectChanges();
      },
      error: (error: any) => {
        console.error(error);
        this.toastr.error('Erro ao tentar carregar Lotes', 'Erro!');
        this.lotesCarregados = true;
        this.cdRef.detectChanges();
        this.spinner.hide();
      },
      complete: () => {
        setTimeout(() => this.spinner.hide());
      }
    });
  }
  
  adicionarLote(): void {
    this.lotes.push(this.criarLote());
  }
  
  
  public criarLote(lote?: Partial<Lote>): FormGroup {
    return this.fb.group({
      id: [lote?.id ?? 0],
      nome: [lote?.nome ?? '', Validators.required],
      preco: [lote?.preco ?? '', Validators.required],
      quantidade: [lote?.quantidade ?? '', Validators.required],
      dataInicio: [this.converterDataApiParaInput(lote?.dataInicio), Validators.required],
      dataFim: [this.converterDataApiParaInput(lote?.dataFim), Validators.required],
      eventoId: [lote?.eventoId ?? this.eventoId],
    });
  }
  
  private converterDataApiParaInput(data?: string | Date | null): string {
    if (!data) return '';
    
    if (data instanceof Date) {
      const ano = data.getFullYear();
      const mes = String(data.getMonth() + 1).padStart(2, '0');
      const dia = String(data.getDate()).padStart(2, '0');
      
      return `${ano}-${mes}-${dia}`;
    }
    
    const dataParte = data.split(' ')[0];
    
    if (/^\d{4}-\d{2}-\d{2}$/.test(dataParte)) {
      return dataParte;
    }
    
    const [dia, mes, ano] = dataParte.split('/');
    
    if (!dia || !mes || !ano) return '';
    
    return `${ano}-${mes.padStart(2, '0')}-${dia.padStart(2, '0')}`;
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
  
  public cssValidatorLote(index: number, campo: string): any {
    const controle = this.lotes.at(index).get(campo);
    
    return {
      'is-invalid': controle?.errors && controle?.touched,
      'is-valid': controle?.valid && controle?.touched
    };
  }
  
  public salvarEvento(): void {
    const camposEvento = [
      'tema',
      'local',
      'dataEvento',
      'qtdPessoas',
      'modalidade',
      'telefone',
      'email',
      'descricao',
    ];
    
    const eventoInvalido = camposEvento.some((campo) => this.form.get(campo)?.invalid);
    
    if (eventoInvalido) {
      camposEvento.forEach((campo) => this.form.get(campo)?.markAsTouched());
      this.toastr.warning('Preencha os campos obrigatórios.', 'Atenção');
      return;
    }
    
    this.spinner.show();
    
    const { lotes, ...eventoForm } = this.form.value;
    
    this.evento = this.estadoSalvar === 'post'
    ? { ...eventoForm }
    : { id: this.evento.id, ...eventoForm };
    
    this.eventoService[this.estadoSalvar](this.evento).subscribe({
      next: (eventoRetorno: Evento) => {
        this.toastr.success(
          this.estadoSalvar === 'post'
          ? 'Evento cadastrado com sucesso!'
          : 'Evento atualizado com sucesso!',
          'Sucesso'
        );
        
        this.evento = eventoRetorno;
        this.eventoId = eventoRetorno.id;
        
        if (this.estadoSalvar === 'post') {
          this.estadoSalvar = 'put';
          this.router.navigate([`/eventos/detalhe/${eventoRetorno.id}`]);
          return;
        }
        
        this.carregarLotes();
      },
      error: (error: any) => {
        console.error(error);
        this.toastr.error('Erro ao salvar Evento', 'Erro!');
        this.spinner.hide();
      },
      complete: () => {
        this.spinner.hide();
      },
    });
  }
  
  public salvarLotes(): void {
    if (this.lotes.invalid) {
      this.lotes.markAllAsTouched();
      this.toastr.warning('Preencha os dados dos lotes.', 'Atenção');
      return;
    }
    
    this.spinner.show();
    
    const lotesParaSalvar = this.lotes.getRawValue().map((lote: any) => ({
      ...lote,
      eventoId: this.eventoId,
      dataInicio: this.converterDataInputParaApi(lote.dataInicio),
      dataFim: this.converterDataInputParaApi(lote.dataFim),
    }));
    
    this.loteService.saveLote(this.eventoId, lotesParaSalvar).subscribe({
      next: (lotesRetorno: Lote[]) => {
        this.toastr.success('Lotes salvos com Sucesso!', 'Sucesso!');
        
        this.lotes.clear();
        
        (lotesRetorno ?? []).forEach((lote: Lote) => {
          this.lotes.push(this.criarLote(lote));
        });
      },
      error: (error: any) => {
        this.toastr.error('Erro ao tentar salvar Lotes.', 'Erro');
        console.error(error);
      },
      complete: () => {
        setTimeout(() => this.spinner.hide());
      }
    });
  }
  
  private converterDataInputParaApi(data: string | Date): string {
    if (!data) return '';
    
    if (data instanceof Date) {
      const dia = String(data.getDate()).padStart(2, '0');
      const mes = String(data.getMonth() + 1).padStart(2, '0');
      const ano = data.getFullYear();
      
      return `${dia}/${mes}/${ano}`;
    }
    
    if (data.includes('/')) {
      return data.split(' ')[0];
    }
    
    const [ano, mes, dia] = data.split('-');
    
    return `${dia}/${mes}/${ano}`;
  }
  
  public removerLote(template: TemplateRef<any>, index: number): void {
    const loteForm = this.lotes.at(index) as FormGroup;
    
    this.loteAtual.id = loteForm.get('id')?.value ?? 0;
    this.loteAtual.nome = loteForm.get('nome')?.value ?? '';
    this.loteAtual.index = index;
    
    if (this.loteAtual.id === 0) {
      this.lotes.removeAt(index);
      return;
    }
    
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }
  
  public confirmDeleteLote(): void {
    this.modalRef?.hide();
    this.spinner.show();
    
    this.loteService.deleteLote(this.eventoId, this.loteAtual.id).subscribe({
      next: () => {
        this.toastr.success('Lote deletado com sucesso', 'Sucesso');
        this.lotes.removeAt(this.loteAtual.index);
      },
      error: (error: any) => {
        this.toastr.error(`Erro ao tentar deletar o Lote ${this.loteAtual.nome}`, 'Erro');
        console.error(error);
      },
      complete: () => {
        this.spinner.hide();
      },
    });
  }
  public declineDeleteLote(): void {
    this.modalRef?.hide();
  }
  
  
  public onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    
    if (!input.files || input.files.length === 0) return;
    
    this.file = input.files[0];
    
    const reader = new FileReader();
    reader.onload = () => {
      this.imagemPreview = reader.result as string;
    };
    
    reader.readAsDataURL(this.file);
    
    this.uploadImagem();
  }

  public uploadImagem(): void {
    if (!this.file || !this.eventoId) return;
    
    this.spinner.show();
    
    this.eventoService.postUpload(this.eventoId, this.file).subscribe({
      next: (eventoRetorno: Evento) => {
        this.evento = eventoRetorno;
        this.form.patchValue(eventoRetorno);

        this.imagemPreview = eventoRetorno.imagemURL
        ? `${API_CONFIG.imageUrl}/${eventoRetorno.imagemURL}`
        : 'assets/img/semImagem.jpeg';

        this.toastr.success('Imagem atualizada com sucesso!', 'Sucesso');
      },
      error: (error: any) => {  
        console.error(error);
        this.toastr.error('Erro ao fazer upload da imagem.', 'Erro!');
        this.spinner.hide();
      },
      complete: () => {
        this.spinner.hide();
      }
    });
  }
}