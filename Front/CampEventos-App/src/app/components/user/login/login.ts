import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { UserLogin } from '../../../models/identity/UserLogin';
import { AccountService } from '../../../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [RouterLink, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class LoginComponent implements OnInit {

  model = {} as UserLogin

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toaster: ToastrService
  ) { }
    
    ngOnInit(): void {}

    public login(): void {
      this.accountService.login(this.model).subscribe({
        next: () => this.router.navigateByUrl('/dashboard'),
        error: (error: any) => {
          if (error.status === 401) {
            this.toaster.error('Usuário ou senha inválidos.');
            return;
          }

          console.error(error);
          this.toaster.error('Erro ao tentar realizar login.');
        }
      });
    }
  }
  