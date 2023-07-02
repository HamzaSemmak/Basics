import { Routes } from '@angular/router';
import { LoginComponent } from '../../Components/Auth/login/login.component';
import { HomeComponent } from 'src/app/Components/Pages/home/home.component';
import { AuthGuard } from 'src/app/Guards/Auth/auth.guard';
import { EmailComponent } from 'src/app/Components/Auth/password/email/email.component';
import { ErrorsComponent } from 'src/app/Components/Pages/errors/errors.component';
import { EmailConfirmComponent } from 'src/app/Components/Auth/password/email-confirm/email-confirm.component';
import { ResetComponent } from 'src/app/Components/Auth/password/reset/reset.component';
import { ConfirmComponent } from 'src/app/Components/Auth/password/confirm/confirm.component';
import { LogoutComponent } from 'src/app/Components/Auth/logout/logout.component';
import { CategoryComponent } from 'src/app/Components/Pages/category/category.component';
import { PayementComponent } from 'src/app/Components/Pages/payement/payement.component';
import { UsersComponent } from 'src/app/Components/Pages/users/users.component';
import { IndexComponent } from 'src/app/Components/Models/Users/index/index.component';
import { CreateComponent } from 'src/app/Components/Models/Users/create/create.component';
import { AdminGuard } from 'src/app/Guards/Admin/admin.guard';
import { UpdateComponent } from 'src/app/Components/Models/Users/update/update.component';

export const Route: Routes = [
    {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'products/category/:item',
        component: CategoryComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'payement/:Key/:Baskets/validate',
        component: PayementComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'users',
        component: UsersComponent,
        canActivate: [AuthGuard, AdminGuard],
        children: [
            { path: '', component: IndexComponent },
            { path: 'create', component: CreateComponent },
            { path: 'update/:key', component: UpdateComponent }
        ]
    },
    // Authentification
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
        path: 'auth/forget-password/password/reset',
        component: ResetComponent
    },
    {
        path: 'auth/forget-password/password/confirm/:key',
        component: ConfirmComponent
    },
    {
        path: 'auth/logout/account',
        component: LogoutComponent
    },
    //404 Page
    { 
        path: '**', 
        redirectTo: '/404/page-not-found',
        pathMatch: 'full'
    },
    { 
        path: '404/page-not-found', 
        component: ErrorsComponent
    }
];