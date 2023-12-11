import { Component ,EventEmitter,OnInit,Output} from '@angular/core';
import { HttpClient,HttpEventType,HttpErrorResponse, HttpClientModule} from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-upload-img',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  templateUrl: './upload-img.component.html',
  styleUrl: './upload-img.component.css'
})
export class UploadImgComponent implements OnInit{
  progress?: number;
  message?: string;
  @Output() public onUploadFinished = new EventEmitter();
  constructor(private http: HttpClient,private router:Router) {  
    if(localStorage.getItem('role')!='User'){
      this.router.navigateByUrl('**');
    }
   }
  ngOnInit() {
  }
    uploadFile = (files: any) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
      this.http.post('http://localhost:5250/api/Upload', formData, {reportProgress: true, observe: 'events'})
      .subscribe({
        next: (event) => {
        if (event.type === HttpEventType.UploadProgress && event.total!=undefined)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }
}
