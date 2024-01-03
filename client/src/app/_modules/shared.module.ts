import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from 'ngx-spinner';

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
  ],
  exports: [BsDropdownModule, TabsModule, ToastrModule, NgxSpinnerModule],
})
export class SharedModule {}
