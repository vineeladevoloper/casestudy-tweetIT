import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Tweet IT';
  constructor(private router:Router){
      console.log(localStorage.getItem('token'));
  }
  contact(){
    this.router.navigateByUrl('contact');
  }
  about(){
    this.router.navigateByUrl('about');
  }
}
