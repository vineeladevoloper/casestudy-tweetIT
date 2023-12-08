import { Component } from '@angular/core';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { HttpClient,HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router,RouterLink, RouterOutlet} from '@angular/router';
import { UserDTO } from '../../../Models/User/user-dto';

@Component({
  selector: 'app-all-posts',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule,RouterLink,RouterOutlet],
  templateUrl: './all-posts.component.html',
  styleUrls: ['./all-posts.component.css']
})
export class AllPostsComponent {
  posts:PostWithId[]=[];
  post:PostWithId;
  searchTerm?:string='';
  user:UserDTO;
  userRole?:any;
  constructor(private http:HttpClient,private router:Router){
    this.post=new PostWithId();
    this.user=new UserDTO();
    this.getAllPosts();
    this.userRole=localStorage.getItem('role');
  }

  onSearch(): void {
    console.log(this.searchTerm);
    if(this.searchTerm==''){
      this.getAllPosts();
    }
    else{
      this.http
      .get<PostWithId[]>('http://localhost:5250/api/Post/SearchPostByTitle/'+this.searchTerm)
      .subscribe((response)=>{
        this.posts=response;
        console.log(this.posts);
      })
    }
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

  viewpost(postId:any){
    console.log(postId);
    if(this.userRole=='User')
      this.router.navigateByUrl('user-dashboard/view-post/'+postId);
    else
      this.router.navigateByUrl('admin-dashboard/view-post/'+postId);
  }
}
