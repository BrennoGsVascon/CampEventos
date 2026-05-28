import { Component, OnInit } from '@angular/core';
import {
  AbstractControlOptions,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

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

  constructor(
    private fb: FormBuilder,
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
    this.spinner.show();

    this.accountService.getUser().subscribe(
      (userRetorno: UserUpdate) => {
        this.userUpdate = userRetorno;

        this.form.patchValue({
          nivel: userRetorno.nivel,
          userName: userRetorno.userName,
          primeiroNome: userRetorno.primeiroNome,
          ultimoNome: userRetorno.ultimoNome,
          email: userRetorno.email,
          phoneNumber: userRetorno.phoneNumber,
          funcao: userRetorno.funcao,
          descricao: userRetorno.descricao,
          imagemURL: userRetorno.imagemURL,
          password: '',
          confirmarPassword: ''
        });

        this.toaster.success('Usuário carregado com sucesso.', 'Sucesso');
      },
      (error) => {
        console.error(error);
        this.toaster.error('Erro ao carregar usuário.', 'Erro');
        this.router.navigate(['/dashboard']);
      }
    ).add(() => this.spinner.hide());
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toaster.error('Preencha os campos obrigatórios corretamente.', 'Erro');
      return;
    }

    this.atualizarUsuario();
  }

  public atualizarUsuario(): void {
    this.userUpdate = {
      ...this.userUpdate,
      ...this.form.value
    };

    if (!this.userUpdate.password) {
      delete this.userUpdate.password;
    }

    this.spinner.show();

    this.accountService.updateUser(this.userUpdate).subscribe(
      (userRetorno) => {
        this.toaster.success('Usuário atualizado com sucesso.', 'Sucesso');

        this.form.patchValue({
          password: '',
          confirmarPassword: ''
        });
      },
      (error) => {
        console.error(error);
        this.toaster.error(
          error?.error || 'Erro ao atualizar usuário.',
          'Erro'
        );
      }
    ).add(() => this.spinner.hide());
  }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorFild.MustMatch('password', 'confirmarPassword')
    };

    this.form = this.fb.group({
      nivel: ['NaoInformado', Validators.required],
      userName: [''],
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      funcao: ['NaoInformado', Validators.required],
      descricao: ['', [Validators.required, Validators.minLength(10)]],
      imagemURL: [''],
      password: ['', Validators.minLength(6)],
      confirmarPassword: ['']
    }, formOptions);
  }

  public resetForm(event: Event): void {
    event.preventDefault();

    this.form.patchValue({
      nivel: this.userUpdate.nivel,
      userName: this.userUpdate.userName,
      primeiroNome: this.userUpdate.primeiroNome,
      ultimoNome: this.userUpdate.ultimoNome,
      email: this.userUpdate.email,
      phoneNumber: this.userUpdate.phoneNumber,
      funcao: this.userUpdate.funcao,
      descricao: this.userUpdate.descricao,
      imagemURL: this.userUpdate.imagemURL,
      password: '',
      confirmarPassword: ''
    });

    this.form.markAsPristine();
    this.form.markAsUntouched();

    this.toaster.info('Alterações canceladas.', 'Perfil');
  }
}