import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { MeasureComponent } from './components/measures/measures.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { AuthGuard } from './services/auth-guard.service';

@NgModule({
    imports: [
        RouterModule.forRoot(
            [
                { path: '', redirectTo: 'home', pathMatch: 'full' },
                { path: 'home', component: HomeComponent },
                { path: 'register', component: RegisterComponent },
                { path: 'login', component: LoginComponent },
                { path: 'measures', canActivate: [AuthGuard], component: MeasureComponent },
                { path: '**', redirectTo: 'home' }
            ],
            { enableTracing: true }
        )
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }