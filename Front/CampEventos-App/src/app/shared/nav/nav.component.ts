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
    NgIf,
    AsyncPipe,
    TitleCasePipe
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {
  isCollapsed = true;

  constructor(
    public accountService: AccountService,
    private router: Router
  ) {}

  toggleNavbar(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  closeNavbar(): void {
    this.isCollapsed = true;
  }

  logout(): void {
    this.accountService.logout();
    this.closeNavbar();
    this.router.navigateByUrl('/user/login');
  }
}
