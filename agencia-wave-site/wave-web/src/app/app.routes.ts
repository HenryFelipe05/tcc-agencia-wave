import { Routes } from '@angular/router'
import { HomeComponent } from './pages/home/home.component';
import { ContactComponent } from './pages/contact/contact.component';
import { GaleryComponent } from './pages/galery/galery.component';
import { LoginComponent } from './pages/login/login.component';
import { SingupComponent } from './pages/singup/singup/singup.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'contato', component: ContactComponent },
    { path: 'galeria', component: GaleryComponent },
    { path: 'login', component: LoginComponent },
    { path: 'criar-conta', component: SingupComponent },
    { path: '**', redirectTo: '' }
];
