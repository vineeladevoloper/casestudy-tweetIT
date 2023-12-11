import { Component } from '@angular/core';
import { CommentNotification } from '../../../Models/Comment/comment-notification';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-comment-notification',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './comment-notification.component.html',
  styleUrl: './comment-notification.component.css'
})
export class CommentNotificationComponent {
notificationList:CommentNotification[]=[];
notification:CommentNotification;
userId?:string;
httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + localStorage.getItem('token'),
  }),
};
constructor(private http:  HttpClient,private activateRoute: ActivatedRoute,private router:Router){
  if(localStorage.getItem('role')!='User'){
    this.router.navigateByUrl('**');
  }
  this.notification=new CommentNotification();
  this.activateRoute.params.subscribe((p) => (this.userId = p['uid']));
  this.getAllNotifications();
}
getAllNotifications(){
  this.http
    .get<CommentNotification[]>('http://localhost:5250/api/CommentNotification/GetCommentNotificationsByUser/'+this.userId,this.httpOptions)
    .subscribe((response)=>{
      this.notificationList=response;
      this.notificationList = response.filter(notification => notification.read === 0);
      console.log(this.notificationList);
    })
}
markAsRead(commentNotificationId: any) {
  console.log(commentNotificationId);
  this.http.put('http://localhost:5250/api/CommentNotification/MarkAsRead/' + commentNotificationId, commentNotificationId,this.httpOptions)
    .subscribe(() => {
      this.getAllNotifications();
    });
}
}
