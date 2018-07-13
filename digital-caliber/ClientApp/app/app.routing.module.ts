import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { MessureComponent } from './components/messures/messures.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { AuthGuard } from './components/services/auth-guard.service';

@NgModule({
    imports: [
        RouterModule.forRoot(
            [
                { path: '', redirectTo: 'home', pathMatch: 'full' },
                { path: 'home', component: HomeComponent },
                { path: 'register', component: RegisterComponent },
                { path: 'login', component: LoginComponent },
                { path: 'messures', canActivate: [AuthGuard], component: MessureComponent },
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