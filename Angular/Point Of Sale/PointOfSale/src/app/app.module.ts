import { NgModule  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { Route } from './Modules/Route/Route';
import { LoginComponent } from './Components/Auth/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorComponent } from './Components/Components/error/error.component';
import { EmailComponent } from './Components/Auth/password/email/email.component';
import { ConfirmComponent } from './Components/Auth/password/confirm/confirm.component';
import { ResetComponent } from './Components/Auth/password/reset/reset.component';
import { EmailConfirmComponent } from './Components/Auth/password/email-confirm/email-confirm.component';
import { ErrorsComponent } from './Components/Pages/errors/errors.component';
import { ToastsComponent } from './Components/Components/toasts/toasts.component';
import { NavbarComponent } from './Components/layouts/navbar/navbar.component';
import { SidebarComponent } from './Components/layouts/sidebar/sidebar.component';
import { LogoutComponent } from './Components/Auth/logout/logout.component';
import { CategoryComponent } from './Components/layouts/category/category.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ErrorComponent,
    EmailComponent,
    ConfirmComponent,
    ResetComponent,
    EmailConfirmComponent,
    ErrorsComponent,
    ToastsComponent,
    NavbarComponent,
    SidebarComponent,
    LogoutComponent,
    CategoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(Route)
  ],
  providers: [],
  bootstrap: [AppComponent],
})

export class AppModule { }
