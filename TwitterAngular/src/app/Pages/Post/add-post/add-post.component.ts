import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-add-post',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './add-post.component.html',
  styleUrl: './add-post.component.css'
})
export class AddPostComponent {
  imageUrl?: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer) {}

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.saveImage(file);
    }
  }

  private saveImage(file: File): void {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      const base64Data = e.target.result.split(',')[1];
  
      // Save the file to the src/assets/uploads folder (for development purposes)
      const filePath = `src/assets/uploads/${file.name}`;
      this.saveFileToFileSystem(base64Data, filePath);
  
      // Display the image
      const safeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(`assets/uploads/${file.name}`);
      this.imageUrl = safeUrl;
  
      // Note: In a real application, you would typically upload the file to a server here.
    };
    reader.readAsDataURL(file);
  }

  private saveFileToFileSystem(data: string, path: string): void {
    const fs = require('fs');
    const base64Data = data.replace(/^data:image\/\w+;base64,/, '');
    const binaryData = Buffer.from(base64Data, 'base64');
  
    fs.writeFileSync(path, binaryData, 'binary');
  }

  private dataURItoBlob(dataURI: string): Blob {
    const byteString = atob(dataURI.split(',')[1]);
    const mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const uint8Array = new Uint8Array(arrayBuffer);

    for (let i = 0; i < byteString.length; i++) {
      uint8Array[i] = byteString.charCodeAt(i);
    }

    return new Blob([arrayBuffer], { type: mimeString });
  }
}
