import { Component } from '@angular/core';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';
import { UserDTO } from '../../../Models/User/user-dto';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-follower',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './follower.component.html',
  styleUrl: './follower.component.css'
})
export class FollowerComponent {
  users:UserDTO[]=[];
  followers:UserDTO[]=[];
  following:UserDTO[]=[];
  user:UserDTO;
  userId?:any;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private http:  HttpClient,private router:Router){
    if(localStorage.getItem('role')!='User'){
      this.router.navigateByUrl('**');
    }
    this.userId=localStorage.getItem('userId');
    this.user=new UserDTO();
    this.getfollowRequest();
    this.getFollowers();
    this.getFollowing();
    }

    getfollowRequest(){
    this.http
    .get<UserDTO[]>('http://localhost:5250/api/Follower/GetPendingFollowRequest/'+this.userId,this.httpOptions)
    .subscribe((response)=>{
      this.users=response;
    })
  }

  acceptRequest(followerId:any){
    const requestBody = {
      userId: this.userId,
      followerId: followerId,
    };
    this.http
    .put('http://localhost:5250/api/Follower/AcceptFollowRequest',requestBody,this.httpOptions)
    .subscribe((response) => {
      console.log(response);
      this.getfollowRequest();
      this.getFollowers();
    });
  }

  getFollowers(){
    this.http
    .get<UserDTO[]>('http://localhost:5250/api/Follower/GetFollowers/'+this.userId,this.httpOptions)
    .subscribe((response)=>{
      this.followers=response;
      console.log(this.followers)
    })
  }

  getFollowing(){
    this.http
    .get<UserDTO[]>('http://localhost:5250/api/Follower/GetFollowing/'+this.userId,this.httpOptions)
    .subscribe((response)=>{
      this.following=response;
      console.log(this.followers)
    })
  }

  postByUser(userID:any){
    this.router.navigateByUrl('user-dashboard/post-by-user/'+userID)
  }
}
