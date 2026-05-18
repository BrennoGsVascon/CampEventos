import { Routes } from '@angular/router';

import { UserComponent } from './components/user/user';
import { LoginComponent } from './components/user/login/login';
import { RegistrationComponent } from './components/user/registration/registration';
import { PerfilComponent } from "./components/user/perfil/perfil.component";

import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { ApresentadoresComponent } from "./components/apresentadores/apresentadores";

import { EventosComponent } from './components/eventos/eventos';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista';

import { ContatosComponent } from "./components/contatos/contatos.component";


export const routes: Routes = [

    {path: 'user', component: UserComponent,
        children: [
            {path:'login', component: LoginComponent },
            {path: 'registration', component: RegistrationComponent},
            
        ]
    },
    {path: 'eventos', redirectTo: 'evento/lista'},
    {path: 'eventos', component: EventosComponent,
        children: [
            { path: '', redirectTo: 'lista', pathMatch: 'full' },
            {path: 'detalhe/:id', component: EventoDetalheComponent},
            {path: 'detalhe', component: EventoDetalheComponent},
            {path: 'lista', component: EventoListaComponent},]},

    {path: 'contatos', component: ContatosComponent},
    {path: 'apresentadores', component: ApresentadoresComponent},
    {path: 'dashboard', component: DashboardComponent},
    {path: 'user/perfil', component: PerfilComponent},
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];
