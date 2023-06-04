import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/Module/Material';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { RegisterComponent } from './Component/register/register.component';
import { LoginComponent } from './Component/login/login.component';
import { HomeComponent } from './Component/home/home.component';
import { UsersComponent } from './Component/users/users.component';
import { UpdatePopUpComponent } from './Component/update-pop-up/update-pop-up.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './Guard/auth.guard';

const Route: Routes = [
  {path: '', component: HomeComponent, canActivate:[AuthGuard]},
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'logout', redirectTo: 'login'},
  {path: 'user', component: UsersComponent, canActivate:[AuthGuard]}
];


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    UsersComponent,
    UpdatePopUpComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot(Route),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
