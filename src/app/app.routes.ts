import { Routes } from '@angular/router';
import { SearchComponent } from './components/search/search.component';
import { HomeComponent } from './components/home/home.component';
import { MediaDetailComponent } from './components/media-detail/media-detail.component';
import { WatchlistComponent } from './components/watchlist/watchlist.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

export const routes: Routes = [

    { path: '', component: HomeComponent},
    { path: 'search', component: SearchComponent},
    { path: 'media/:id/:type', component: MediaDetailComponent },
    { path: 'watchlist', component: WatchlistComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },

];
