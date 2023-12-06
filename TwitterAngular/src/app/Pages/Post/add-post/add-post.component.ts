import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PostWithoutIdDTO } from '../../../Models/Post/post-without-id-dto';
import { UploadImgComponent } from '../upload-img/upload-img.component';
import { RouterOutlet,Router } from '@angular/router';

@Component({
  selector: 'app-add-post',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule,UploadImgComponent,RouterOutlet],
  templateUrl: './add-post.component.html',
  styleUrl: './add-post.component.css'
})
export class AddPostComponent {
  post:PostWithoutIdDTO;
  create:boolean;
  response: any;
  msg?:string='';
  constructor(private http:HttpClient,private router:Router){
    this.post=new PostWithoutIdDTO;
    this.response={dbPath: ''};
    this.create=false;
  }
  uploadFinished = (event: any) => { 
      this.response = event; 
      this.post.img=this.response.dbPath;
    }
 
   onSubmit(): void {
    console.log(this.post);
      this.http.post('http://localhost:5250/api/Post/AddPost', this.post)
      .subscribe(response => {
        console.log('Post created successfully', response);
        this.msg='Post created successfully';
        this.router.navigateByUrl('all-posts');
      }, error => {
        console.error('Error creating post', error);
        this.msg='Error creating post';
      });
  }
}
