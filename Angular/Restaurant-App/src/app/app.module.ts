import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { CreateComponent } from './components/create/create.component';
import { UpdateComponent } from './components/update/update.component';
import { IndexComponent } from './components/index/index.component';
import { HttpClientModule } from '@angular/common/http';

const route: Routes = [
  { path: '', component: IndexComponent },
  { path: 'Create', component: CreateComponent },
  { path: 'Update/:id', component: UpdateComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    CreateComponent,
    UpdateComponent,
    IndexComponent
  ],
  exports: [
    RouterModule
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(route),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
