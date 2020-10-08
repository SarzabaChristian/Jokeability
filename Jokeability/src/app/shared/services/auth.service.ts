import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';
import {catchError,tap} from 'rxjs/operators';
import { Router } from '@angular/router';

export interface AuthResponse {
    id:number,
    username: string,
    lastName: string,
    firstName: string,
    token: string,
    expiry: string,
}

@Injectable({providedIn:'root'})
export class AuthService {
    user =new BehaviorSubject<User>(null);
    baseUrl=environment.APIAddress + 'auth/';
    private tokenExpirationTimer:any;
    constructor(private http:HttpClient,private router:Router){}


    register(newUser: any){
        return this.http.post<AuthResponse>(this.baseUrl + 'register',newUser)
                        .pipe(catchError(this.handleErrors),
                              tap(responseData=>{this.handleAuthenticatedUser(responseData)})
                        );
    }

    login(loginUser: any) {
        return this.http.post<AuthResponse>(this.baseUrl + 'login',loginUser)
                        .pipe(catchError(this.handleErrors),
                              tap(responseData=>{this.handleAuthenticatedUser(responseData)})
                        )
    }

    logout() {
        this.user.next(null);
        localStorage.removeItem('userData');
        if(this.tokenExpirationTimer){
            clearTimeout(this.tokenExpirationTimer);
          }
        this.tokenExpirationTimer = null;
        this.router.navigate(['/auth']);
    }

    autoLogin() {
        const userData=this.getLoginUserData();
        if(!userData) {
            return;
        }

        const loadedUser=new User(userData.id,userData.username,userData.firstName,userData.lastName,userData._token,new Date(userData._tokenExpirationDate));
        if(loadedUser.token){
            this.user.next(loadedUser);
            const expirationDate=new Date(userData._tokenExpirationDate).getTime() - new Date().getTime();
            this.autoLogout(expirationDate);
        }
    }
    private handleAuthenticatedUser(responseDate: AuthResponse) {
        const expirationDate=new Date(new Date().getTime() + (+responseDate.expiry * 1000));
        const user=new User(responseDate.id,responseDate.username,responseDate.firstName,responseDate.lastName,responseDate.token,expirationDate);
        this.user.next(user);
        localStorage.setItem('userData',JSON.stringify(user));
        this.router.navigate(['/home']);
    }

    private autoLogout(expirationDate: number){
        this.tokenExpirationTimer = setTimeout(()=>{
            this.logout()
        },expirationDate)
    }

    getLoginUserData(){
        const userData: {
            id:number,
            username:string,
            firstName:string,
            lastName:string,
            _token: string,
            _tokenExpirationDate: string
        }=JSON.parse(localStorage.getItem('userData'));

        return userData;
    }

    private handleErrors(errorRespone: HttpErrorResponse) {
        let errorMsg='An unknown error occured';
       
        switch (errorRespone.error.message){
            case 'EMAIL_EXISTS':
                errorMsg = 'This email already exists';
                break;
              case 'EMAIL_NOT_FOUND':
                errorMsg = 'Email doesnt exist';
                break;
              case 'INVALID_PASSWORD':
                errorMsg = 'Invalid password. Please make sure your password is correct';
                break;
              default:
                break;
        }
        return throwError(errorMsg);
    }
}