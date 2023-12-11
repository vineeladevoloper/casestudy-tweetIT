import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router ,ActivatedRoute} from '@angular/router';
import { HttpClientModule, HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css'
})
export class EditUserComponent {
  user:UserDTO;
  userId?:string;
  role?:any;
  errMsg: string = '';
  isUserExist: boolean = false;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private activateRoute: ActivatedRoute,private http:HttpClient){
    if(localStorage.getItem('role')==null){
      this.router.navigateByUrl('**');
    }
    this.user=new UserDTO();
    this.role=localStorage.getItem('role');
    this.activateRoute.params.subscribe((p) => (this.userId = p['uid']));
    console.log(this.userId);
    this.search();
  }
  search() {
    this.http
      .get<UserDTO>(
        'http://localhost:5250/api/User/GetUserById/' + this.userId,this.httpOptions
      )
      .subscribe((response) => {
        console.log(response);
        if (response != null) {
          this.user = response;
          this.isUserExist = true;
          this.errMsg = '';
        } else {
          this.errMsg = 'Invalid User Id';
          this.isUserExist = false;
        }
      });
  }
  edit() {
    this.http
      .put('http://localhost:5250/api/User/EditUser', this.user,this.httpOptions)
      .subscribe((response) => {
        console.log(response);
      });
    if(localStorage.getItem('role')=="User"){
      this.router.navigateByUrl('user-dashboard');
    }
    else{
      this.router.navigateByUrl('admin-dashboard');
    }
  }
  upgrade(userId:any){
    this.http
      .put('http://localhost:5250/api/User/UpgradeRequest/'+ userId,userId,this.httpOptions)
      .subscribe((response) => {
        this.search();
        console.log(response);
      });
  }
  
}
