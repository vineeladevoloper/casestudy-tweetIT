import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { CommonModule } from '@angular/common';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';
import { AdminAction } from '../../../Models/User/admin-action';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-notification',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule],
  templateUrl: './admin-notification.component.html',
  styleUrl: './admin-notification.component.css'
})
export class AdminNotificationComponent {
  users:UserDTO[]=[];
  action:AdminAction;
  adminId:any;
  errmsg:string='';
  searchTerm?:string='';
  user:UserDTO;
  userId?:any;
  searchUserId: string = '';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
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
        this.users=this.users.filter(user=>user.status=='Requested');
        console.log(this.users);
        console.log(this.searchUserId);
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
          this.users = this.users.filter(user => user.role !== 'Admin');
          this.users=this.users.filter(user=>user.status=='Requested');
          console.log(this.users);
        })
      }
    }

    upgradefn(userID:any){
      this.adminId=localStorage.getItem('userId');
      this.action.adminUserId=this.adminId;
      this.action.userIdTo=userID;
      console.log(this.action);
      this.http.put('http://localhost:5250/api/User/UpgradeUserRequest',this.action,this.httpOptions)
      .subscribe((response)=>{
        console.log(response);
        this.getAllUsers();
      })
    }
  
    rejectfn(userID:any){
      this.adminId=localStorage.getItem('userId');
      this.action.adminUserId=this.adminId;
      this.action.userIdTo=userID;
      console.log(this.action);
      this.http.put('http://localhost:5250/api/User/RejectUserRequest',this.action,this.httpOptions)
      .subscribe((response)=>{
        console.log(response);
        this.getAllUsers();
      })
    }
}
