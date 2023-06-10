import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './Components/Layouts/sidebar/sidebar.component';
import { NavbarComponent } from './Components/Layouts/navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { Route } from './Modules/Route';
import { LoginComponent } from './Components/Auth/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { ForgetPasswordComponent } from './Components/Auth/forget-password/forget-password.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    NavbarComponent,
    LoginComponent,
    ForgetPasswordComponent
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
