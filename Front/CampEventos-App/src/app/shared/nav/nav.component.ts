import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, NgbDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {
  constructor(private router: Router) {}

  isCollapsed = true;

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
  }

