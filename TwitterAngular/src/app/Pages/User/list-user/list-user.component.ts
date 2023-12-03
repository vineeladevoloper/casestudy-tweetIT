import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-list-user',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  templateUrl: './list-user.component.html',
  styleUrl: './list-user.component.css'
})
export class ListUserComponent {
  users:UserDTO[]=[];
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  user:UserDTO;
  userId?:any;
  constructor(private http:  HttpClient,private router:Router){
    this.user=new UserDTO();
    this.getAllUsers();
  }
  getAllUsers(){
    this.http
    .get<UserDTO[]>('http://localhost:5250/api/User/GetAllUsers',this.httpOptions)
    .subscribe((response)=>{
      this.users=response;
      console.log(this.users);
    })
  }
  delete(userID: any) {
    this.userId = userID;
    console.log(typeof this.userId);
    console.log(this.userId);
    this.http
      .delete('http://localhost:5250/api/User/Deleteuser/' + this.userId)
      .subscribe((response) => {
        console.log(response);
        this.getAllUsers();
      });
  }
  edit(userID: any) {
    this.router.navigateByUrl('edit-user/' + userID);
  }
}
