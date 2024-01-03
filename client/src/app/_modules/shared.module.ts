import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'top-right',
    }),
    TabsModule.forRoot(),
    NgxSpinnerModule.forRoot({ type: 'square-spin' }),
    FileUploadModule,
  ],
  exports: [BsDropdownModule, TabsModule, ToastrModule, NgxSpinnerModule, FileUploadModule],
})
export class SharedModule { }
