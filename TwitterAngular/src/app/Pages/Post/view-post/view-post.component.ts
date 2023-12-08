import { CommonModule } from '@angular/common';
import { HttpClientModule ,HttpClient} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router ,ActivatedRoute} from '@angular/router';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { Comment } from '../../../Models/Comment/comment';
import { Like } from '../../../Models/like';
import { UserDTO } from '../../../Models/User/user-dto';

@Component({
  selector: 'app-view-post',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './view-post.component.html',
  styleUrl: './view-post.component.css'
})
export class ViewPostComponent {
  postId?:number;
  post:PostWithId;
  user:UserDTO;
  likeId?:number;
  showLikes: boolean = false;
  likeStatus: boolean = false;
  like:Like;
  likes:Like[]=[];
  showComments: boolean = false;  
  comment:Comment;
  comments:Comment[]=[];
  newCommentText: string = '';
  role?:any;
  constructor(private http:HttpClient,private activateRoute: ActivatedRoute,private router:Router){
    this.post=new PostWithId();
    this.comment=new Comment();
    this.user=new UserDTO();
    this.like=new Like();
    this.role=localStorage.getItem('role');
    this.activateRoute.params.subscribe((p) => (this.postId = p['pid']));
    this.getPost();
      }

  getPost(){
    this.http
    .get('http://localhost:5250/api/Post/GetPostById/'+this.postId)
    .subscribe((response)=>{
      this.post=response;
    })
  }


  public createImgPath = (url: any) => { 
    return `http://localhost:5250/${url}`;
  }

  getAllcomments(){
    this.http
    .get<Comment[]>('http://localhost:5250/api/Comment/GetAllCommentsForPost/'+this.postId)
    .subscribe((response)=>{
      this.comments=response;
      console.log(this.comments);
    })
  }
  addComment() {
    this.comment.commentText=this.newCommentText;
    this.comment.postId=this.postId;
    this.comment.userId=localStorage.getItem('userId')?.toString();
    this.http
      .post('http://localhost:5250/api/Comment/AddComment',this.comment)
      .subscribe((response)=>{
        console.log(response);
        // this.comments.push(response);
        this.getAllcomments();
        
      })
      this.newCommentText='';
      
  }

  toggleComments() {
    this.showComments = !this.showComments;
    if(this.showComments==true){
      this.getAllcomments();
      this.showLikes=false;
    }
  }
  toggleLikes() {
    this.showLikes = !this.showLikes;
    if(this.showLikes==true){
      this.checkLikeStatus();
      this.getLikes();
      this.showComments=false;
    }
  }

  checkLikeStatus() {
    this.like.userId=localStorage.getItem('userId')?.toString();
    this.http.get('http://localhost:5250/api/Like/GetLikeByPostAndUser/'+this.postId+'/'+this.like.userId).subscribe(
      (like: any) => {
        this.likeStatus = !!like;
      },
      error => {
        console.error('Error checking like status', error);
      }
    );
  }

  addLike() {
    if (this.likeStatus) {
      this.removeLike();
    } else {
      this.performLike();
    }
  }

  performLike() {
    this.like.postId=this.postId;
    this.like.userId=localStorage.getItem('userId')?.toString();
    this.http.post('http://localhost:5250/api/Like/AddLike',this.like).subscribe(
      response => {
        this.like=response;
        this.likeId=this.like.likeId;
        this.likeStatus = true;
        this.getLikes();
      },
      error => {
        console.error('Error adding like', error);
      }
    );
  }

  removeLike() {
    this.http.delete('http://localhost:5250/api/Like/DeleteLikeByUserPost/'+this.postId+'/'+this.like.userId).subscribe(
      response => {
        console.log('Like removed successfully');
        this.getLikes();
      },
      );
      this.likeStatus = false;
      this.toggleLikes();
  }

  getLikes(){
    this.http
    .get<Like[]>('http://localhost:5250/api/Like/GetLikesByPost/'+this.postId)
    .subscribe((response)=>{
      this.likes=response;
      console.log(this.likes);
    })
  }
}
