import { Component } from '@angular/core';
import { Login } from '../../../Models/User/login';
import { CommonModule } from '@angular/common';
import { FormsModule,NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient,HttpClientModule } from '@angular/common/http';
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
  errMsg: string = '';
  httpResponse: any;
  constructor(private router:Router,private http:HttpClient){
    this.login=new Login();
  }
    
  onSubmit(): void {
    console.log(JSON.stringify(this.login, null, 2));
    this.http.post('http://localhost:5250/api/User/Validate',this.login)
    .subscribe((response)=>{
      this.httpResponse = response;
        console.log(this.httpResponse);
        if (this.httpResponse.token != null) {
          //store token in local storage
          localStorage.setItem('token', this.httpResponse.token);
          if (this.httpResponse.role == 'Admin') {
            this.router.navigateByUrl('admin-dashboard');
            console.log("admin");
            //redirect to customer dashboard
          } else if (this.httpResponse.role == 'User') {
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
  }

  redirectToRegister() {
    this.router.navigateByUrl('register');
  }
}
