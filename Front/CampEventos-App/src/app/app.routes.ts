import { Routes } from '@angular/router';
import { EventosComponent } from './components/eventos/eventos';
import { ContatosComponent } from "./components/contatos/contatos.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { PerfilComponent } from "./components/perfil/perfil.component";
import { ApresentadoresComponent } from "./components/apresentadores/apresentadores";



export const routes: Routes = [
    {path: '', redirectTo: 'eventos', pathMatch: 'full'},

    {path: 'eventos', component: EventosComponent},
    {path: 'contatos', component: ContatosComponent},
    {path: 'apresentadores', component: ApresentadoresComponent},
    {path: 'dashboard', component: DashboardComponent},
    {path: 'perfil', component: PerfilComponent},
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];
