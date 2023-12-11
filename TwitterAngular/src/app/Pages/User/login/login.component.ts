import { Component } from '@angular/core';
import { Login } from '../../../Models/User/login';
import { CommonModule } from '@angular/common';
import { FormsModule,NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient,HttpClientModule, HttpHeaders } from '@angular/common/http';
import { UserDTO } from '../../../Models/User/user-dto';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  login:Login;
  user:any;
  errMsg: string = '';
  httpResponse: any;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private http:HttpClient){
    this.login=new Login();
  }
    
  contact(){
    this.router.navigateByUrl('contact');
  }
  about(){
    this.router.navigateByUrl('about');
  }

  onSubmit(): void {
    this.http.post('http://localhost:5250/api/User/Validate',this.login,this.httpOptions)
    .subscribe((response)=>{
      this.httpResponse = response;
        console.log(this.httpResponse);
        if (this.httpResponse.token != null) {
          localStorage.setItem('token', this.httpResponse.token);
          localStorage.setItem('userId',this.httpResponse.userId);
          localStorage.setItem('role',this.httpResponse.role);
          if (this.httpResponse.role == 'Admin') 
          {
            this.router.navigateByUrl('admin-dashboard');
            console.log("admin");
          } 
          else if (this.httpResponse.role == 'User') 
          {
            this.router.navigateByUrl('user-dashboard');
            console.log("user");
          }
        } else {
          this.errMsg = 'Invalid Credentials';
          console.log(this.errMsg);
        }
    })
  }

  onReset(form: NgForm): void {
    form.reset();
    this.errMsg='';
  }

  redirectToRegister() {
    this.router.navigateByUrl('register');
  }
}
