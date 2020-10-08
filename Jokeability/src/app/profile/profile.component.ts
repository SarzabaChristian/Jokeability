import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { JokerProfile } from '../shared/models/jokerprofile.model';
import { AuthService } from '../shared/services/auth.service';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  id:number;
  isPersonalProfile=false;
  jokerProfile:JokerProfile;
  constructor(private activatedRoute:ActivatedRoute,
              private authService:AuthService,
              private userService:UserService,
              private router: Router) { }

  ngOnInit(): void {
    this.jokerProfile=new JokerProfile;
    this.activatedRoute.params.subscribe((params:Params) => {
      this.id=params['id']   
    })
    if(!this.id){
      const user=this.authService.getLoginUserData();
      this.id=user.id;
      this.isPersonalProfile=true;
    }
    this.userService.getJokerProfile(this.id).subscribe(response=> {            
      this.jokerProfile=response;    
    })
  }

  
  onNewJoke() {
    this.router.navigate(['profile/new']);
  }

}
