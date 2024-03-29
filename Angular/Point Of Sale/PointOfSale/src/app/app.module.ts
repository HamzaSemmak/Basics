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
import { SidebarComponent } from './Components/Layouts/sidebar/sidebar.component';
import { NavbarComponent } from './Components/Layouts/navbar/navbar.component';
import { CategoryComponent } from './Components/Components/category/category.component';
import { LogoutComponent } from './Components/Auth/logout/logout.component';
import { HomeComponent } from './Components/Pages/home/home.component';
import { ProductComponent } from './Components/Components/product/product.component';
import { CategoryComponent as CategoryComponentPages } from './Components/Pages/category/category.component';
import { PanierComponent } from './Components/Components/panier/panier.component';
import { PayementComponent } from './Components/Pages/payement/payement.component';
import { SpinnerComponent } from './Components/Components/spinner/spinner.component';
import { UsersComponent } from './Components/Pages/users/users.component';
import { IndexComponent as UsersIndes } from './Components/Models/Users/index/index.component';
import { CreateComponent as UsersCreate } from './Components/Models/Users/create/create.component';
import { UpdateComponent as UsersUpdate } from './Components/Models/Users/update/update.component';
import { ShowComponent as UsersShow} from './Components/Models/Users/show/show.component';
import { OrdersComponent } from './Components/Pages/orders/orders.component';
import { IndexComponent as OrdersIndex } from './Components/Models/Orders/index/index.component';
import { ShowComponent as OrdersShow } from './Components/Models/Orders/show/show.component';
import { ProductsComponent } from './Components/Pages/products/products.component';
import { IndexComponent as ProductIndex } from './Components/Models/Products/index/index.component';
import { ShowComponent as ProductsShow } from './Components/Models/Products/show/show.component';

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
    CategoryComponent,
    HomeComponent,
    ProductComponent,
    CategoryComponentPages,
    PanierComponent,
    PayementComponent,
    SpinnerComponent,
    UsersComponent,
    UsersIndes,
    UsersCreate,
    UsersUpdate,
    UsersShow,
    OrdersComponent,
    OrdersIndex,
    OrdersShow,
    ProductsComponent,
    ProductIndex,
    ProductsShow,
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
