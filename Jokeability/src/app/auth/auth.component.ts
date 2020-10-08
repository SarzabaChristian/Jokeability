import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthResponse, AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  isLoginMode=true;
  private user:any={};
  error:string=null;
  constructor(private _authService: AuthService,private router:Router) { }
  ngOnInit(): void {
    
  }

  onSubmit(form: NgForm) {    
    if(form.invalid)
      return;
    let authObs: Observable<AuthResponse>;       
    this.user.username=form.value.username;
    this.user.password=form.value.password;
    if(this.isLoginMode){     
     
      authObs=this._authService.login(this.user);
    } else {      
      this.user.firstName=form.value.firstName;
      this.user.lastName=form.value.lastName;      
      authObs=this._authService.register(this.user);
    }

    authObs.subscribe(responseData => {
                        this.router.navigate(['/home']);
                        // console.log(responseData);
                      },errorMessage => {
                        this.error = errorMessage;                        
                      });

    form.reset();                    
  }

  onSwitchMode(){
    this.isLoginMode = !this.isLoginMode    
  }
}
