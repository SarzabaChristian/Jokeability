import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { exhaustMap, take } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
    constructor(private authService:AuthService){}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return this.authService.user.pipe(
            take(1),
            exhaustMap(user => {
                if(!user){
                    return next.handle(req);
                }
                const headerSettings: {[name: string]: string | string[]; } = {};
                headerSettings['Authorization']='Bearer ' + user.token;
                headerSettings['Content-Type'] = 'application/json';
                const newHeader = new HttpHeaders(headerSettings);
                // const modRequest=req.clone({headers: new HttpHeaders().set('Authorization','Bearer ' + user.token)})
                const modRequest = req.clone({headers: newHeader});
                return next.handle(modRequest);
            })
        )
    }

}