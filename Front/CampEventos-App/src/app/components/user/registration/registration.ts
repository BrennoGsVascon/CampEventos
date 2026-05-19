import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { ValidatorFild } from '../../../helpers/ValidatorFild';

@Component({
  selector: 'app-registration',
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './registration.html',
  styleUrl: './registration.scss',
})
export class RegistrationComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
      return this.form.controls;
  }

  constructor(public fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators : ValidatorFild.MustMatch('senha','confirmeSenha')
    };

    this.form = this.fb.group({

      primeiroNome : ['',Validators.required],
      ultimoNome : ['',Validators.required],
      email : ['', [Validators.required, Validators.email]],
      userName : ['',Validators.required],
      senha : ['',[Validators.required, Validators.minLength(6)]],
      confirmeSenha : ['',Validators.required,],
    },formOptions);
  }

  public resetForm(): void {
    this.form.reset();
  }

}
