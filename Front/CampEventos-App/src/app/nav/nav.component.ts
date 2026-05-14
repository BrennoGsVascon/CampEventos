import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, NgbDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {

  isCollapsed = true;

  toggleNavbar(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  closeNavbar(): void {
    this.isCollapsed = true;
  }
}
