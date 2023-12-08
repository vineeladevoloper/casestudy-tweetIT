import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterOutlet } from '@angular/router';
import { UserDTO } from '../../../Models/User/user-dto';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule,RouterOutlet,HttpClientModule,RouterLink],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent {
  admin:UserDTO;
  adminId:any;
  constructor(private http:HttpClient,private router:Router,private route: ActivatedRoute){
      this.admin=new UserDTO();
      this.adminId=localStorage.getItem('userId');
      console.log(this.adminId);
      this.http.get('http://localhost:5250/api/User/GetUserById/'+this.adminId)
      .subscribe((response)=>{
        this.admin=response;
        console.log(this.admin);
      })
  }

  shouldDisplayImage(): boolean {
    // Check if the current route is AdminDashboardComponent
    return this.route.snapshot.routeConfig?.path === 'admin-dashboard';
  }
  logout(){
    localStorage.removeItem('userId');
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigateByUrl('login');
  }
}
