import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class Environment {
  /*
  |--------------------------------------------------------------------------
  | Application Environment
  |--------------------------------------------------------------------------
  |
  | Here you can find all global variables 
  | To use the environment configurations we have defined,
  | we need to import the environments file EnvironmentModule
  |
  */

  static readonly ENV: string = 'Production';
  static readonly APP_NAME: string = 'e-store';
  static readonly ATHAUR: string = 'Hamza Semmak';
  static readonly VERSION: string = '1.0';
  static readonly Date: Date = new Date()

  /* API */
  static readonly API: string = 'https://dummyjson.com';
  static readonly API_PRODUCTS: string = Environment.API + '/products';
}
