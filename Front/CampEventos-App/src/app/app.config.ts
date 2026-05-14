import { ApplicationConfig, LOCALE_ID, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

import { provideToastr } from 'ngx-toastr';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { importProvidersFrom } from '@angular/core';
import { NgxSpinnerModule } from 'ngx-spinner';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(NgxSpinnerModule.forRoot()),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(),
    provideAnimationsAsync(),
    provideToastr({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),
   
    { provide: LOCALE_ID, useValue: 'pt-BR' },

  
  ]
};
