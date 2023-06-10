import { Routes } from '@angular/router';
import { LoginComponent } from '../Components/Auth/login/login.component';
import { ForgetPasswordComponent } from '../Components/Auth/forget-password/forget-password.component';

export const Route: Routes = [
    {
        path: '',
        component: LoginComponent
    },
    {
        path: 'auth/forget-password',
        component: ForgetPasswordComponent
    }
];