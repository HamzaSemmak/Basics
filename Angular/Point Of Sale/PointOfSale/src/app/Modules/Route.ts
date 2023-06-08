import { Routes } from '@angular/router';
import { AppComponent } from '../app.component';
import { LoginComponent } from '../Components/Auth/login/login.component';
import { RegisterComponent } from '../Components/Auth/register/register.component';

export const Route: Routes = [
    {
        path: '',
        component: LoginComponent
    },
    {
        path: 'auth/login',
        component: LoginComponent
    },
    {
        path: 'auth/register',
        component: RegisterComponent
    }
];