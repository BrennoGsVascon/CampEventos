import { Routes } from '@angular/router';

import { UserComponent } from './components/user/user';
import { LoginComponent } from './components/user/login/login';
import { RegistrationComponent } from './components/user/registration/registration';
import { PerfilComponent } from './components/user/perfil/perfil.component';

import { HomeComponent } from './components/home/home.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ApresentadoresComponent } from './components/apresentadores/apresentadores';

import { EventosComponent } from './components/eventos/eventos';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista';

import { ContatosComponent } from './components/contatos/contatos.component';
import { authGuard } from './guard/auth-guard';


export const routes: Routes = [
  {
    path: 'user',
    component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ]
  },

  {
    path: 'eventos',
    canActivate: [authGuard],
    component: EventosComponent,
    children: [
      { path: '', redirectTo: 'lista', pathMatch: 'full' },
      { path: 'lista', component: EventoListaComponent },
      { path: 'detalhe', component: EventoDetalheComponent },
      { path: 'detalhe/:id', component: EventoDetalheComponent },
    ]
  },

  { path: 'contatos', component: ContatosComponent, canActivate: [authGuard] },
  { path: 'apresentadores', component: ApresentadoresComponent, canActivate: [authGuard] },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
  { path: 'user/perfil', component: PerfilComponent, canActivate: [authGuard] },

  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];
