import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule,  Optional, SkipSelf, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';

import { throwIfAlreadyLoaded } from './module-import-guard';
import { RequestInterceptor } from './services/http.interceptors';

import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { MeasureComponent } from './components/measures/measures.component';
import { ContainerComponent } from './components/shared/container/container.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { FooterComponent } from './components/footer/footer.component';

//Services
import { AuthGuard } from './services/auth-guard.service';
import { ConfigService } from './services/config.service';
import { SpinnerService } from './services/spinner.service';
import { AccountService } from './services/account.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    MeasureComponent,
    ContainerComponent,
    SpinnerComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    CommonModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl },
    SpinnerService,
    AuthGuard,
    AccountService,
    HttpClient,
    { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'es' },
    
    ConfigService,
    {
        // Request that configuration loading be done at app-initialisation time (prior to rendering)
        provide: APP_INITIALIZER,
        useFactory: configFactory,
        deps: [ConfigService],
        multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor( @Optional() @SkipSelf() parentModule: AppModule) {
    throwIfAlreadyLoaded(parentModule, 'AppModule');
  }
}

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

export function configFactory(cfg: ConfigService) {
  return () => cfg.loadConfigurationData();
}