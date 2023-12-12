import { Component } from '@angular/core';
import { HttpClient, HttpClientModule ,HttpHeaders} from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule ,NgForm } from '@angular/forms';
import { PostWithoutIdDTO } from '../../../Models/Post/post-without-id-dto';
import { UploadImgComponent } from '../upload-img/upload-img.component';
import { RouterOutlet,Router ,ActivatedRoute} from '@angular/router';
import { UserDTO } from '../../../Models/User/user-dto';

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
  userId?:string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private http:HttpClient,private router:Router,private activateRoute: ActivatedRoute){
    if(localStorage.getItem('role')!='User'){
      this.router.navigateByUrl('**');
    }
    this.post=new PostWithoutIdDTO;
    this.activateRoute.params.subscribe((p) => (this.userId = p['uid']));
    this.response={dbPath: ''};
    this.post.userId=this.userId;
    this.create=false;
  }
  uploadFinished = (event: any) => { 
      this.response = event; 
      this.post.img=this.response.dbPath;
    }
 
    onSubmit(): void {
      console.log(this.post);
      this.http.post('http://localhost:5250/api/Post/AddPost', this.post, this.httpOptions)
        .subscribe(response => {
          console.log('Post created successfully', response);
          this.msg = 'Post created successfully';
          this.router.navigateByUrl('user-dashboard/all-posts');
        }, error => {
          console.error('Error creating post', error);
          this.msg = 'Error creating post';
        });
    }
}
