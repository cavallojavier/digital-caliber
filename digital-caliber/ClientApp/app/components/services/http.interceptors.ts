import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { tap, catchError, finalize } from "rxjs/operators";

@Injectable()
export class RequestInterceptor implements HttpInterceptor {

  constructor() {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    let authToken = localStorage.getItem('auth_token');

    request = request.clone({
      setHeaders: {
        'Authorization': `Bearer ${authToken}`,
        'Content-Type': 'application/json',
        'Cache-control': 'no-cache',
        'Expires': '0',
        'Pragma': 'no-cache'
      }
    });

    return next.handle(request).pipe(
        tap((event: HttpEvent<any>) => {
          if (event instanceof HttpResponse) {
          }
        }), 
        catchError((error: any) => {
            if (error.status >= 200 && error.status < 300) {
          
              const res = new HttpResponse({
                body: null,
                headers: error.headers,
                status: error.status,
                statusText: error.statusText,
                //url: err.url
              });
              
              return of(res);
            } else {
              return Observable.throw(error);
            }
        }),
        finalize(() => {

        })
    )
  };
}