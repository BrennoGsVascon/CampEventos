import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { AbstractControlOptions, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { ValidatorFild } from '../../../helpers/ValidatorFild';
import { User } from '../../../models/identity/User';
import { AccountService } from '../../../services/account.service';

import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  imports: [RouterLink, ReactiveFormsModule, FormsModule],
  templateUrl: './registration.html',
  styleUrl: './registration.scss',
})

export class RegistrationComponent implements OnInit {

  user = {} as User;

  form!: FormGroup;

  get f(): any {
      return this.form.controls;
  }

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router: Router,
              private toaster: ToastrService
  ) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators : ValidatorFild.MustMatch('password','confirmePassword')
    };

    this.form = this.fb.group({

      primeiroNome : ['',Validators.required],
      ultimoNome : ['',Validators.required],
      email : ['', [Validators.required, Validators.email]],
      userName : ['',Validators.required],
      password : ['',[Validators.required, Validators.minLength(4)]],
      confirmePassword : ['',Validators.required,],
    },formOptions);
  }

  
  register(): void{
    this.user = {...this.form.value};
    this.accountService.register(this.user).subscribe(
      () => this.router.navigateByUrl('/dashboard'),
      (error: any)=> this.toaster.error(error.error)
    )
  }
  
  public resetForm(): void {
    this.form.reset();
  }
}
