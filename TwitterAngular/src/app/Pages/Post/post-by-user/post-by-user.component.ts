import { CommonModule } from '@angular/common';
import { HttpClientModule,HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { Router ,ActivatedRoute} from '@angular/router';
import { UserDTO } from '../../../Models/User/user-dto';

@Component({
  selector: 'app-post-by-user',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './post-by-user.component.html',
  styleUrl: './post-by-user.component.css'
})
export class PostByUserComponent {
  posts:PostWithId[]=[];
  userId?:string;
  post:PostWithId;
  searchTerm?:string='';
  user:UserDTO;
  role:any;
  constructor(private http:HttpClient,private activateRoute: ActivatedRoute,private router:Router){
    this.post=new PostWithId();
    this.user=new UserDTO();
    this.activateRoute.params.subscribe((p) => (this.userId = p['uid']));
    this.role=localStorage.getItem('role');
    this.getPosts();
  }
  onSearch(): void {
    console.log(this.searchTerm);
    if(this.searchTerm==''){
      this.getPosts();
    }
    else{
      this.http
      .get<PostWithId[]>('http://localhost:5250/api/Post/SearchPostsByTitleAndUserId/'+this.userId+'/'+this.searchTerm)
      .subscribe((response)=>{
        this.posts=response;
        console.log(this.posts);
      })
    }
  }
  getPosts(){
    this.http
    .get<PostWithId[]>('http://localhost:5250/api/Post/ListPostByUserId/'+this.userId)
    .subscribe((response)=>{
      this.posts=response;
      console.log(this.posts);
    })
  }
  public createImgPath = (url: any) => { 
        return `http://localhost:5250/${url}`;
      }

  edit(postId:any){
console.log(postId);
this.router.navigateByUrl('user-dashboard/edit-post/'+postId);
  }
  delete(postId:any){
    console.log(postId);
    this.http
      .delete('http://localhost:5250/api/Post/DeletePost/'+postId)
      .subscribe((response)=>{
        console.log(response);
      })
    this.router.navigateByUrl('user-dashboard/all-posts');
  }

}
