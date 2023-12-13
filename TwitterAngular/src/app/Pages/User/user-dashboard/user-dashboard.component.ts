import { CommonModule } from '@angular/common';
import { HttpClientModule ,HttpClient,HttpHeaders} from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { UserDTO } from '../../../Models/User/user-dto';
import { Router} from '@angular/router';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [CommonModule,RouterOutlet,HttpClientModule,RouterLink],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css'
})
export class UserDashboardComponent {
  user:UserDTO;
  userId:any;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private http:HttpClient,private router:Router){
    if(localStorage.getItem('role')!='User'){
      this.router.navigateByUrl('**');
    }
      this.user=new UserDTO();
      this.userId=localStorage.getItem('userId');
      // console.log(this.userId);
      this.http.get('http://localhost:5250/api/User/GetUserById/'+this.userId,this.httpOptions)
      .subscribe((response)=>{
        this.user=response;
        // console.log(this.user);
      })
  }
  logout(){
    localStorage.removeItem('userId');
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigateByUrl('login');
  }
}
