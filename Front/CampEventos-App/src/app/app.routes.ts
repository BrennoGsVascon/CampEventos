import { Routes } from '@angular/router';
import { EventosComponent } from './eventos/eventos';


export const routes: Routes = [
    {
        path: '',
        redirectTo: 'eventos',
        pathMatch: 'full'
    },

    {
        path: 'eventos',
        component: EventosComponent
    }
];
