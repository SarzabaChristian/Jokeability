import { NgModule } from "@angular/core";
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home.component';
import { JokeListComponent } from './joke-list/joke-list.component';
import { JokeItemComponent } from './joke-item/joke-item.component';

@NgModule({
    declarations:[HomeComponent, JokeListComponent, JokeItemComponent],
    imports: [SharedModule,RouterModule.forChild([{path:'',component:HomeComponent}])],
    exports: [RouterModule]
})

export class HomeModule{}