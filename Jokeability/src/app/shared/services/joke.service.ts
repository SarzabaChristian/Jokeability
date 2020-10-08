import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Joke } from '../models/joke.model';




@Injectable({providedIn:'root'})
export class JokeService {
    baseURL=environment.APIAddress;
    constructor(private http:HttpClient) {}
    listJokesChanged = new Subject<Joke[]>();
    private listJokes:Joke[]=[];

    fetchJokeInServer() {
        return this.http.get<Joke[]>(this.baseURL + "joke")
                        .pipe(
                            tap(response=> {
                                this.setNewJokes(response);
                            })
                        );  
    }
    getJokes() {
        return this.listJokes.slice();
    }

    setNewJokes(jokes: Joke[]) {
        this.listJokes=jokes;
        this.listJokesChanged.next(this.listJokes.slice());
    }
}