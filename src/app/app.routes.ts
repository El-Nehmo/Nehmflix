import { Routes } from '@angular/router';
import { SearchComponent } from './components/search/search.component';
import { HomeComponent } from './components/home/home.component';
import { MediaDetailComponent } from './components/media-detail/media-detail.component';

export const routes: Routes = [

    { path: '', component: HomeComponent},
    { path: 'search', component: SearchComponent},
    { path: 'media/:id/:type', component: MediaDetailComponent },

];
