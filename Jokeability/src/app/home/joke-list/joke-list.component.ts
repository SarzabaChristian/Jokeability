import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Joke } from 'src/app/shared/models/joke.model';
import { JokeService } from 'src/app/shared/services/joke.service';

@Component({
  selector: 'app-joke-list',
  templateUrl: './joke-list.component.html',
  styleUrls: ['./joke-list.component.css']
})
export class JokeListComponent implements OnInit,OnDestroy {
  jokes: Joke[]=[];
  sub: Subscription;
  isLoadingData=true;
  constructor(private jokeService:JokeService) { }
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  ngOnInit(): void {   
    this.jokeService.fetchJokeInServer().subscribe(response => {
       this.isLoadingData=false;
    });
    this.sub=this.jokeService.listJokesChanged.subscribe(response=>{
      this.jokes=response;
    })
    this.jokes=this.jokeService.getJokes();
  }

}
