import { Component } from '@angular/core';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { HttpClient,HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { UserDTO } from '../../../Models/User/user-dto';

@Component({
  selector: 'app-all-posts',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  templateUrl: './all-posts.component.html',
  styleUrl: './all-posts.component.css'
})
export class AllPostsComponent {
  posts:PostWithId[]=[];
  post:PostWithId;
  
  constructor(private http:HttpClient){
    this.post=new PostWithId();
    this.getAllPosts();
  }
  getAllPosts(){
    this.http
    .get<PostWithId[]>('http://localhost:5250/api/Post/GetAllPosts')
    .subscribe((response)=>{
      this.posts=response;
      console.log(this.posts);
    })
  }
  public createImgPath = (url: any) => { 
        return `http://localhost:5250/${url}`;
      }

  getName(userId:any){
    this.http
    .get<UserDTO>('http://localhost:5250/api/User/GetUserById/'+userId,)
    .subscribe((response)=>{
      console.log(response)  
      if(response!=null)
        return response.userName;
      else
      return "User not found";
    })
  }
}
