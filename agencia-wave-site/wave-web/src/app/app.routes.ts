import { Routes } from '@angular/router'
import { HomeComponent } from './pages/home/home.component';
import { ContactComponent } from './pages/contact/contact.component';
import { GaleryComponent } from './pages/galery/galery.component';
import { SubscriptionComponent } from './pages/subscription/subscription.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'contato', component: ContactComponent },
    { path: 'galeria', component: GaleryComponent },
    { path: 'assinaturas', component: SubscriptionComponent },
    { path: '**', redirectTo: '' }
];
