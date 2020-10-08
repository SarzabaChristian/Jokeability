import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Router } from '@angular/router';
import { Subject } from 'rxjs/internal/Subject';
import { environment } from 'src/environments/environment.prod';
import { Joke } from '../models/joke.model';
import { JokerProfile } from '../models/jokerprofile.model';
import { JokeService } from './joke.service';

@Injectable({providedIn:"root"})

export class UserService {
    baseURL=environment.APIAddress + 'user/';
    constructor(private http:HttpClient,private jokeService:JokeService,private router:Router) {}
    listJokeChanged: Subject<Joke[]>;
    private listJokes:Joke[];

    voteJoke(model: any) {
        return this.http.post<string>(this.baseURL + 'vote',model)
                        .subscribe(response=>{
                                        this.jokeService.fetchJokeInServer().subscribe();
                                        alert("Yey! Succefully voted!");                                        
                                    },error=> {
                                        alert("something went wrong");
                                        console.log(HttpResponse);
                                    });
    }

    getJokerProfile(id: number) {
        return this.http.get<JokerProfile>(this.baseURL + id);
    }


    createJoke(model: any) {
        return this.http.post<Joke>(this.baseURL + 'addjoke',model).subscribe(response=> {           
            // alert('Succesfully created new joke');           
        },error=> {
            alert('something went wrong');
        })
    }
}