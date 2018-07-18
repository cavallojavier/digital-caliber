import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppModuleShared } from './app.shared.module';
import { AppRoutingModule } from './app.routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    //AppModuleShared
  ],
  imports: [
    BrowserModule,
    AppModuleShared,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }