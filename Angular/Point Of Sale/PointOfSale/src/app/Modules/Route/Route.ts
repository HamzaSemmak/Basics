import { Routes } from '@angular/router';
import { LoginComponent } from '../../Components/Auth/login/login.component';
import { ForgetPasswordComponent } from '../../Components/Auth/forget-password/forget-password.component';
import { HomeComponent } from 'src/app/Components/Pages/home/home.component';
import { AuthGuard } from 'src/app/Guards/Auth/auth.guard';

export const Route: Routes = [
    {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'auth/forget-password',
        component: ForgetPasswordComponent
    },
    {
        path: 'auth/login',
        component: LoginComponent
    }
];