import { Routes } from '@angular/router';
import { LoginComponent } from '../../Components/Auth/login/login.component';
import { HomeComponent } from 'src/app/Components/Pages/home/home.component';
import { AuthGuard } from 'src/app/Guards/Auth/auth.guard';
import { EmailComponent } from 'src/app/Components/Auth/password/email/email.component';
import { ErrorsComponent } from 'src/app/Components/errors/errors.component';
import { EmailConfirmComponent } from 'src/app/Components/Auth/password/email-confirm/email-confirm.component';

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
    },
    {
        path: 'auth/forget-password/email-confirm/:email',
        component: EmailConfirmComponent
    },
    { 
        path: '**', 
        component: ErrorsComponent
    }
];