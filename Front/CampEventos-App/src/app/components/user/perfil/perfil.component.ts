import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { ValidatorFild } from '../../../helpers/ValidatorFild';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [TituloComponent, ReactiveFormsModule],
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

   form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {

    this.form = this.fb.group({
      
      nivel: ['', Validators.required],
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', Validators.required],
      funcao: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.minLength(10)]],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmarSenha: ['', Validators.required],
    }, {
      validators: ValidatorFild.MustMatch('senha', 'confirmarSenha')
    });
  }

  public resetForm(): void {
    this.form.reset();
  }
}