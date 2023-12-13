import { CommonModule } from '@angular/common';
import { HttpClientModule ,HttpClient, HttpHeaders} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router ,ActivatedRoute} from '@angular/router';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { Comment } from '../../../Models/Comment/comment';
import { Like } from '../../../Models/Like/like';
import { UserDTO } from '../../../Models/User/user-dto';
import { CommentNotificationWithoutId } from '../../../Models/Comment/comment-notification-without-id';

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
  commentNotification:CommentNotificationWithoutId;
  role?:any;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private http:HttpClient,private activateRoute: ActivatedRoute,private router:Router){
    if(localStorage.getItem('role')==null){
      this.router.navigateByUrl('**');
    }
    this.post=new PostWithId();
    this.comment=new Comment();
    this.commentNotification=new CommentNotificationWithoutId();
    this.user=new UserDTO();
    this.like=new Like();
    this.role=localStorage.getItem('role');
    this.activateRoute.params.subscribe((p) => (this.postId = p['pid']));
    this.getPost();
      }

  getPost(){
    this.http
    .get('http://localhost:5250/api/Post/GetPostById/'+this.postId,this.httpOptions)
    .subscribe((response)=>{
      this.post=response;
    })
  }


  public createImgPath = (url: any) => { 
    return `http://localhost:5250/${url}`;
  }

  getAllcomments(){
    this.http
    .get<Comment[]>('http://localhost:5250/api/Comment/GetAllCommentsForPost/'+this.postId,this.httpOptions)
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
      .post('http://localhost:5250/api/Comment/AddComment',this.comment,this.httpOptions)
      .subscribe((response)=>{
        console.log(response);
        this.getAllcomments();
        
      })
      this.commentNotification.commentText=this.newCommentText;
      this.commentNotification.postId=this.postId;
      this.commentNotification.receiverId=this.post.userId;
      this.commentNotification.senderId=this.comment.userId;
      this.newCommentText='';
      this.http
      .post('http://localhost:5250/api/CommentNotification/AddCommentNotification',this.commentNotification,this.httpOptions)
      .subscribe((response)=>{
        console.log(response);
        this.getAllcomments();
        
      })
      
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
    this.checkLikeStatus();
    if(this.showLikes==true){
      this.getLikes();
      this.showComments=false;
    }
  }

  checkLikeStatus() {
    this.like.userId=localStorage.getItem('userId')?.toString();
    console.log(this.postId)
    console.log(this.like.userId)
    this.http.get('http://localhost:5250/api/Like/GetLikeByPostAndUser/'+this.postId+'/'+this.like.userId,this.httpOptions)
    .subscribe((response)=>{
        console.log(response);
        if(response==this.postId){
          console.log(true);
          this.likeStatus=true;
        }
        else{
          this.likeStatus=false;
        }
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
    this.http.post('http://localhost:5250/api/Like/AddLike',this.like,this.httpOptions).subscribe(
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
    this.http.delete('http://localhost:5250/api/Like/DeleteLikeByUserPost/'+this.postId+'/'+this.like.userId,this.httpOptions).subscribe(
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
    .get<Like[]>('http://localhost:5250/api/Like/GetLikesByPost/'+this.postId,this.httpOptions)
    .subscribe((response)=>{
      this.likes=response;
      console.log(this.likes);
    })
  }
}
