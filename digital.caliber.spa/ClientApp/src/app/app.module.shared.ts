import { NgModule,  Optional, SkipSelf, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppComponent } from './app.component';

import { throwIfAlreadyLoaded } from './module-import-guard';

import { ContainerComponent } from './components/shared/container/container.component';
import { MeasureRowComponent } from './components/shared/measure-row/measure-row.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { ModalComponent } from './components/shared/modals/modal.component';

//Services
import { AuthGuard } from './services/auth-guard.service';
import { ConfigService } from './services/config.service';
import { SpinnerService } from './services/spinner.service';
import { AccountService } from './services/account.service';
import { RequestInterceptor } from './services/http.interceptors';

//Pipes
import { decimalToStringPipe } from './components/shared/pipes/decimal-display.pipe';

@NgModule({
  declarations: [
    ContainerComponent,
    MeasureRowComponent,
    ModalComponent,
    SpinnerComponent,
    decimalToStringPipe
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports:[
    ContainerComponent,
    MeasureRowComponent,
    ModalComponent,
    SpinnerComponent,
    decimalToStringPipe
  ],
  providers: [
    AuthGuard,
    SpinnerService,
    AccountService,
    HttpClient,
    { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true },
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
export class SharedModule { 
  constructor( @Optional() @SkipSelf() parentModule: SharedModule) {
    throwIfAlreadyLoaded(parentModule, 'SharedModule');
  }
}

export function configFactory(cfg: ConfigService) {
  return () => cfg.loadConfigurationData();
}