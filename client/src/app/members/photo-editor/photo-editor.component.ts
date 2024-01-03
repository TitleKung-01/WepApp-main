import { Component, Input, OnInit } from '@angular/core';
import { faTrashCan, faStar } from '@fortawesome/free-regular-svg-icons';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  faTrashCan = faTrashCan
  faStar = faStar
  @Input() member: Member | undefined
  uploader: FileUploader | undefined
  hasBaseDropZoneOver = false
  baseUrl = environment.apiUrl
  user: User | undefined | null
  constructor(private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    })
  }
  ngOnInit(): void {
    this.initUploader()
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e
  }
  initUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-image',
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024 //MB to bytes
    })
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }
    this.uploader.onSuccessItem = (item, response, status, header) => {
      if (response) {
        const photo = JSON.parse(response)
        this.member?.photos.push(photo)
        if (photo.isMain && this.user && this.member) {
          this.user.photoUrl = photo.url
          this.member.mainPhotoUrl = photo.url
          this.accountService.setCurrentUser(this.user)
        }
      }
    }
  }
}