import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';
import { Post } from './post';
import { UploadComponent } from './upload/upload.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet,UploadComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Tweet IT';
  response: any;
  post:Post;
  create:boolean;
  constructor(private router:Router){
      console.log(localStorage.getItem('token'));
      this.response={dbPath: ''};
      this.post=new Post();
      this.create=false;
  }
  contact(){
    this.router.navigateByUrl('contact');
  }
  about(){
    this.router.navigateByUrl('about');
  }
  // uploadFinished = (event: any) => { 
  //   this.response = event; 
  // }
  // onCreate = () => {
  //   this.post.img=this.response.dbPath;
  //   this.create=true;
  //   }
  //   public createImgPath = () => { 
  //     return `http://localhost:5250/${this.post.img}`;
  //   }

}
