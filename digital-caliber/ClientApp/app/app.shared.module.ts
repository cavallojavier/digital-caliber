//Angular
import { LOCALE_ID, NgModule,  Optional, SkipSelf, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app.routing.module';
//Services
import { ConfigService } from './components/services/config.service';
import { SpinnerService } from './components/services/spinner.service';

//Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { MessureComponent } from './components/messures/messures.component';
import { ContainerComponent } from './components/shared/container/container.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { FooterComponent } from './components/footer/footer.component';
import { throwIfAlreadyLoaded } from './module-import-guard';

//Services
import { AccountService } from './components/services/account.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        RegisterComponent,
        LoginComponent,
        MessureComponent,
        ContainerComponent,
        SpinnerComponent,
        FooterComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        HttpClientModule,
        FormsModule,
        AppRoutingModule
    ],
    exports: [
        ContainerComponent
    ],
    providers: [
        SpinnerService,
        AccountService,
        HttpClient,
        { provide: LOCALE_ID, useValue: 'es' },
        
        ConfigService,
        {
            // Request that configuration loading be done at app-initialisation time (prior to rendering)
            provide: APP_INITIALIZER,
            useFactory: configFactory,
            deps: [ConfigService],
            multi: true
        }
        
    ]
})
export class AppModuleShared {
    constructor( @Optional() @SkipSelf() parentModule: AppModuleShared) {
        throwIfAlreadyLoaded(parentModule, 'SharedModule');
    }
}

export function configFactory(cfg: ConfigService) {
    return () => cfg.loadConfigurationData();
}