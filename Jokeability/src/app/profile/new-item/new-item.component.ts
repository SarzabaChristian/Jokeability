import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { HasUnsavedData } from 'src/app/shared/guards/can-deactivate.guard';
import { AuthService } from 'src/app/shared/services/auth.service';
import { JokeService } from 'src/app/shared/services/joke.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-new-item',
  templateUrl: './new-item.component.html',
  styleUrls: ['./new-item.component.css']
})
export class NewItemComponent implements OnInit,HasUnsavedData {
  userProfile:any={};

  newJoke= {
    jokerId: 0,
    title:'',
    description:'',
  };
  
  constructor(private authService:AuthService,
    private userService: UserService,
    private router:Router,
    private activatedRoute: ActivatedRoute,
    private jokeService:JokeService) { }

  ngOnInit(): void {
    this.userProfile=this.authService.getLoginUserData();
  }

  onCreateJoke() {   
    if(confirm("Are you sure you want to create this joke?")) {
      this.newJoke.jokerId=this.userProfile.id;
      this.userService.createJoke(this.newJoke);  
      this.jokeService.fetchJokeInServer().subscribe(response => {
        this.newJoke.title='';
        this.newJoke.description='';
        this.router.navigate(['home']);
      });;
      
    }   
  }

  onBack() {
    this.router.navigate(['../'], {relativeTo: this.activatedRoute});
  }

  hasUnsavedData(): boolean {    
    if (this.newJoke.title.length > 0 || this.newJoke.description.length > 0 ) {
      return confirm('Do you want to discard the changes?');
    }

    return true;
    // return true;
  }
}
