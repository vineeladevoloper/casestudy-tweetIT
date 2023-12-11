import { Component } from '@angular/core';
import { Router ,ActivatedRoute} from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { UploadImgComponent } from '../upload-img/upload-img.component';

@Component({
  selector: 'app-edit-post',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule,UploadImgComponent],
  templateUrl: './edit-post.component.html',
  styleUrl: './edit-post.component.css'
})
export class EditPostComponent {
  post:PostWithId;
  postId?:number;
  response: any;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private activateRoute: ActivatedRoute,private http:HttpClient){
    if(localStorage.getItem('role')!='User'){
      this.router.navigateByUrl('**');
    }
    this.post=new PostWithId();
    this.activateRoute.params.subscribe((p) => (this.postId = p['pid']));
    console.log(this.postId);
    this.search();
  }
  uploadFinished = (event: any) => { 
    this.response = event; 
    this.post.img=this.response.dbPath;
  }
  search() {
    this.http
      .get<PostWithId>(
        'http://localhost:5250/api/Post/GetPostById/' + this.postId,this.httpOptions
      )
      .subscribe((response) => {
        console.log(response);
        this.post=response;
      });
  }
  edit() {
    console.log(this.post);
    this.http
      .put('http://localhost:5250/api/Post/EditPost', this.post,this.httpOptions)
      .subscribe((response) => {
        console.log(response);
      });
    this.router.navigateByUrl('user-dashboard/all-posts');
  }
}
