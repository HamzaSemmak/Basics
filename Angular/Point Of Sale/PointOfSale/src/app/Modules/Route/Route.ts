import { Routes } from '@angular/router';
import { LoginComponent } from '../../Components/Auth/login/login.component';
import { HomeComponent } from 'src/app/Components/Pages/home/home.component';
import { AuthGuard } from 'src/app/Guards/Auth/auth.guard';
import { EmailComponent } from 'src/app/Components/Auth/password/email/email.component';

export const Route: Routes = [
    {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'auth/login',
        component: LoginComponent
    },
    {
        path: 'auth/forget-password/email/:email',
        component: EmailComponent
    }
];