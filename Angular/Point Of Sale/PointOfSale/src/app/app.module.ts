import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './Components/Layouts/sidebar/sidebar.component';
import { NavbarComponent } from './Components/Layouts/navbar/navbar.component';
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
import { ErrorsComponent } from './Components/errors/errors.component';
import { ToastsComponent } from './Components/Components/toasts/toasts.component';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    NavbarComponent,
    LoginComponent,
    ErrorComponent,
    EmailComponent,
    ConfirmComponent,
    ResetComponent,
    EmailConfirmComponent,
    ErrorsComponent,
    ToastsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(Route)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }