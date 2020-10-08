import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAuthenticated=false;
  constructor(private authService:AuthService) { }

  ngOnInit(): void {
    this.authService.user.subscribe(resp=>{this.isAuthenticated=!!resp})
  }
  onLogout(){
    this.authService.logout();
  }
}
