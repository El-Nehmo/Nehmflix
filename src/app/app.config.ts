import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { routes } from './app.routes';
import { SearchComponent } from './components/search/search.component';
import { MediaDetailComponent } from './components/media-detail/media-detail.component';
import { WatchlistComponent } from './components/watchlist/watchlist.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';


export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    importProvidersFrom(FormsModule),
    importProvidersFrom(ReactiveFormsModule),
    provideHttpClient(),
    SearchComponent,
    MediaDetailComponent,
    WatchlistComponent,
    LoginComponent,
    RegisterComponent
  ]
};



