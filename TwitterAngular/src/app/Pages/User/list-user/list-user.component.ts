import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';
import { AdminAction } from '../../../Models/User/admin-action';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-list-user',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule],
  templateUrl: './list-user.component.html',
  styleUrl: './list-user.component.css'
})
export class ListUserComponent {
  users:UserDTO[]=[];
  action:AdminAction;
  adminId:any;
  errmsg:string='';
  searchTerm?:string='';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  user:UserDTO;
  userId?:any;
  searchUserId: string = '';
  constructor(private http:  HttpClient,private router:Router){
    if(localStorage.getItem('role')!='Admin'){
      this.router.navigateByUrl('**');
    }
    this.user=new UserDTO();
    this.action=new AdminAction();
    this.getAllUsers();
    }
  getAllUsers(){
    this.http
    .get<UserDTO[]>('http://localhost:5250/api/User/GetAllUsers',this.httpOptions)
    .subscribe((response)=>{
      this.users=response;
      this.users = this.users.filter(user => user.role !== 'Admin');
      console.log(this.users);
      console.log(this.searchUserId);
    })
  }

  postByUser(userID:any){
    this.router.navigateByUrl('admin-dashboard/post-by-user/'+userID)
  }

 blockfn(userID:any){
    this.adminId=localStorage.getItem('userId');
    this.action.adminUserId=this.adminId;
    this.action.userIdTo=userID;
    console.log(this.action);
    this.http.post('http://localhost:5250/api/User/BlockUser',this.action,this.httpOptions)
    .subscribe((response)=>{
      console.log(response);
      this.getAllUsers();
    })
  }
  onSearch(){
    console.log(this.searchTerm);
    if(this.searchTerm==''){
      this.getAllUsers();
    }
    else{
      this.http
      .get<UserDTO[]>('http://localhost:5250/api/User/GetUsersByName/'+this.searchTerm,this.httpOptions)
      .subscribe((response)=>{
        this.users=response;
        console.log(this.users);
      })
    }
  }
}
