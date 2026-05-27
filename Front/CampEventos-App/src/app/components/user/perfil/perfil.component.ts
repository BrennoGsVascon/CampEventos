import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { ValidatorFild } from '../../../helpers/ValidatorFild';
import { AccountService } from '../../../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserUpdate } from '../../../models/identity/UserUpdate';
import { Router } from '@angular/router';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [TituloComponent, ReactiveFormsModule],
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  userUpdate = {} as UserUpdate;
   form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder,
              public accountService: AccountService,
              private router: Router,
              private toaster: ToastrService,
              private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.validation();
    this.carregarUsuario();
  }

  private carregarUsuario(): void {
    this.accountService.getUser()
  }

  onSubmit(): void {
    if(this.form.invalid) {
      return;
    }
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

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }
}