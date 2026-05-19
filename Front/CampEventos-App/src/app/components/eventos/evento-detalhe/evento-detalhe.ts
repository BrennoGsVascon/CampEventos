import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  imports: [ReactiveFormsModule],
  templateUrl: './evento-detalhe.html',
  styleUrl: './evento-detalhe.scss',
})
export class EventoDetalheComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({

      tema : ['', [Validators.required,Validators.minLength(4),Validators.maxLength(50),]],
      local : ['', Validators.required],
      dataEvento : ['', Validators.required],
      qtdPessoas : ['', [Validators.required,Validators.max(10000),]],
      modalidade : ['', Validators.required],
      telefone : ['', Validators.required],
      email: ['', [ Validators.required, Validators.email]],
      descricao: ['', Validators.required],

    });
  }

  public resetForm(): void {
    this.form.reset();
  } 
}