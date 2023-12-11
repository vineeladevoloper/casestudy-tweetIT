import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { ActivatedRoute ,Router} from '@angular/router';
import { HttpClient,HttpClientModule ,HttpHeaders} from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
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
  constructor(private activateRoute:ActivatedRoute,private http:HttpClient,private router:Router){
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
}
