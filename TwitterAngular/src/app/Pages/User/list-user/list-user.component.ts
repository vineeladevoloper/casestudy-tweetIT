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
  searchflag:boolean;
  action:AdminAction;
  adminId:any;
  errmsg:string='';
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
    this.user=new UserDTO();
    this.action=new AdminAction();
    this.searchflag=false;
    this.getAllUsers();
    }
  getAllUsers(){
    this.http
    .get<UserDTO[]>('http://localhost:5250/api/User/GetAllUsers',this.httpOptions)
    .subscribe((response)=>{
      this.users=response;
      console.log(this.users);
      console.log(this.searchUserId);
    })
  }

  upgradefn(userID:any){
    this.adminId=localStorage.getItem('userId');
    this.action.adminUserId=this.adminId;
    this.action.userIdTo=userID;
    console.log(this.action);
    this.http.post('http://localhost:5250/api/User/UpgradeUser',this.action,this.httpOptions)
    .subscribe((response)=>{
      console.log(response);
      this.getAllUsers();
    })
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
  applySearchFilter() {
    console.log("In search");
    console.log(this.searchUserId);
    this.http
    .get<UserDTO>('http://localhost:5250/api/User/GetUserById/'+this.searchUserId,this.httpOptions)
    .subscribe((response)=>{
      console.log(response)
      if(response==null){
        this.errmsg="User not found";
        console.log(this.errmsg);
        this.searchflag=false;
      }
      else{
        this.user=response;
        this.errmsg='';
        this.searchflag=true;
      }
    })
  }
}
