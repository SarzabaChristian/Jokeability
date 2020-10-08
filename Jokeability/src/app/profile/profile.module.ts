import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthGuard } from '../shared/guards/auth.guard';
import { SharedModule } from '../shared/shared.module';
import { ProfileComponent } from './profile.component';
import { NewItemComponent } from './new-item/new-item.component';
import { CanDeactivateGuard } from '../shared/guards/can-deactivate.guard';

@NgModule({
    declarations: [ProfileComponent, NewItemComponent],
    imports: [SharedModule,RouterModule.forChild([
                                                    {path:'',component:ProfileComponent,canActivate:[AuthGuard]},
                                                    {path:'new',component:NewItemComponent,canActivate:[AuthGuard], canDeactivate: [CanDeactivateGuard]},
                                                    {path:':id',component:ProfileComponent,canActivate:[AuthGuard]}
                                                    
                                                ])],
    exports: [RouterModule]
})
export class ProfileModule {}