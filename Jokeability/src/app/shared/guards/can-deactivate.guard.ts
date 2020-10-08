import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

export interface HasUnsavedData {
  hasUnsavedData(): boolean;
}

@Injectable({providedIn: 'root'})
export class CanDeactivateGuard implements CanDeactivate<HasUnsavedData> {
  canDeactivate(component: HasUnsavedData,
                currentRoute: ActivatedRouteSnapshot,
                currentState: RouterStateSnapshot,
                nextState?: RouterStateSnapshot): boolean
  {
    return component.hasUnsavedData();
  }
}
