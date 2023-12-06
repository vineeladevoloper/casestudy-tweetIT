import { Component } from '@angular/core';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { HttpClient,HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-all-posts',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule],
  templateUrl: './all-posts.component.html',
  styleUrl: './all-posts.component.css'
})
export class AllPostsComponent {
  posts:PostWithId[]=[];
  post:PostWithId;
  searchTerm?:string='';
  constructor(private http:HttpClient){
    this.post=new PostWithId();
    this.getAllPosts();
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

}
