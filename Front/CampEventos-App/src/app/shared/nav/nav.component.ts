import { AsyncPipe, NgIf, TitleCasePipe } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-nav',
  standalone: true,

  imports: [
    RouterLink,
    RouterLinkActive,

    NgbDropdownModule,
    TitleCasePipe,
    NgIf,
    AsyncPipe
  ],

  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {

  isCollapsed = true;

  constructor(
    public accountService: AccountService,
    private router: Router
  ) { }

  toggleNavbar(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  closeNavbar(): void {
    this.isCollapsed = true;
  }

  showMenu(): boolean {
    return !this.router.url.includes('/user/login')
        && !this.router.url.includes('/user/registration');
  }

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/user/login');
  }
}