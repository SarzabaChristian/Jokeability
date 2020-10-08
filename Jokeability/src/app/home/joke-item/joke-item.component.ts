import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Joke } from 'src/app/shared/models/joke.model';
import { User } from 'src/app/shared/models/user.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-joke-item',
  templateUrl: './joke-item.component.html',
  styleUrls: ['./joke-item.component.css']
})
export class JokeItemComponent implements OnInit {
  isLiable=false;
  @Input('joke') joke: Joke;
  voteRequest: any={};
  currentUser: User;

  constructor(private authService:AuthService,
              private userService: UserService,
              private router: Router,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.authService.user.subscribe(response => {       
      this.isLiable=!!response;
      this.currentUser=response;
    });    
  }

  onVoteFunny() {
    if(!this.isLiable){
      alert('Please login before you vote.')
      return;
    }

    if(this.currentUser.id==this.joke.jokerID){
      alert('You cannot vote your own joke. For data reliability')
      return;
    }

    this.voteRequest.jokeId=this.joke.id;
    this.voteRequest.userId=this.currentUser.id;
    this.voteRequest.jokerId=this.joke.jokerID;
    this.voteRequest.reaction="Funny";
    this.userService.voteJoke(this.voteRequest);    
  }

  onVoteAww() {
    if(!this.isLiable){
      alert('Please login before you vote.')
      return;
    }

    if(this.currentUser.id==this.joke.jokerID){
      alert('You cannot vote your own joke. For data reliability')
      return;
    }
    this.voteRequest.jokeId=this.joke.id;
    this.voteRequest.userId=this.currentUser.id;
    this.voteRequest.jokerId=this.joke.jokerID;
    this.voteRequest.reaction="Aww";      
    this.userService.voteJoke(this.voteRequest);
  }  

  onViewJoker() {
    this.router.navigate(['/profile/' + this.joke.jokerID]);
  }
}
