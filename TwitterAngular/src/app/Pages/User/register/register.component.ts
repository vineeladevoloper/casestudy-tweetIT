import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { FormsModule,NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient ,HttpClientModule,HttpHeaders} from '@angular/common/http';
import * as emailjs from 'emailjs-com';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  user:UserDTO;
  errMsg:string='';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private http:HttpClient){
    this.user=new UserDTO();
  }
  
  contact(){
    this.router.navigateByUrl('contact');
  }
  about(){
    this.router.navigateByUrl('about');
  }

  onSubmit(): void {
    console.log(JSON.stringify(this.user));
    console.log(this.user);
    this.http.post('http://localhost:5250/api/User/Register',this.user,this.httpOptions)
    .subscribe(
      (response:any)=>{
      console.log(response);
      this.router.navigateByUrl('login');
      this.sendEmail();
    },
    (error: any) => {
      // Handle error here
      console.error('Error:', error.error);
      this.errMsg=error.error;
    }
    );
  }

  onReset(form: NgForm): void {
    this.errMsg='';
    form.reset();
  }
  redirectToLogin() {
    this.router.navigateByUrl('login');
  }

  sendEmail() {
    const templateParams = {
      to_name: this.user.name,
      from_name: 'TweetIT',
      
      to_mail: this.user.userEmail
    };
  
    emailjs.init("a4qr0GhxxEBz2cGnj");
    emailjs.send('service_9ynvzfd', 'template_3zqbeoi', templateParams)
      .then((response) => {
        console.log('Email sent successfully:', response);
      }, (error) => {
        console.error('Error sending email:', error);
      });
  }
}
