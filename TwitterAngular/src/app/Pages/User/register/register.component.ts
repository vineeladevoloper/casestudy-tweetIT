import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { FormsModule,NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient ,HttpClientModule,HttpHeaders} from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  user:UserDTO;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private http:HttpClient){
    this.user=new UserDTO();
  }
  
  onSubmit(): void {
    console.log(JSON.stringify(this.user));
    console.log(this.user);
    this.http.post('http://localhost:5250/api/User/Register',this.user,this.httpOptions)
    .subscribe((response)=>{
      console.log(response);
    })
    this.router.navigateByUrl('list-users');
  }

  onReset(form: NgForm): void {
    form.reset();
  }
  redirectToLogin() {
    this.router.navigateByUrl('login');
  }
}
