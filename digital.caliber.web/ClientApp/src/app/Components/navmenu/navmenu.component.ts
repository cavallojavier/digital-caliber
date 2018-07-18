import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from "rxjs";
import { filter } from 'rxjs/operators';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.scss']
})
export class NavMenuComponent implements OnInit {
    isSignedIn: boolean = false;
    username: string = '';

    constructor(private router: Router, private accountService: AccountService){
        this.router.events.pipe(
            filter(event => event instanceof NavigationEnd)
        )
        .subscribe(res => {
            this.checkUserStatus();
            console.log('route changed');
        });
    }

    ngOnInit(){
        
    }

    logout(e) {
        e.preventDefault();

        this.accountService.logout()
        .toPromise()
        .then(() => {
            this.router.navigate(['/login']);
        })
        .catch((err: any) => {
        });
    }

    checkUserStatus(){
        this.isSignedIn = this.accountService.isLoggedIn();
        if(this.isSignedIn){
            let user = this.accountService.getLoggedUser();
            if(user)
                this.username = user.firstName;
        }
    }
}
