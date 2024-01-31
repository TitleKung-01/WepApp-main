import { CommonModule } from '@angular/common';
import { Component, Input, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faClock, faPaperPlane } from '@fortawesome/free-regular-svg-icons';
import { NgxLongPress2Module } from 'ngx-long-press2';
import { TimeagoModule } from 'ngx-timeago';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    FontAwesomeModule,
    TimeagoModule,
    FormsModule,
    NgxLongPress2Module,
  ],
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css'],
})
export class MemberMessagesComponent {
  @Input() username?: string;
  @Input() messages: Message[] = [];
  faClock = faClock;
  faPaperPlane = faPaperPlane;

  @ViewChild('messageForm') messageForm?: NgForm;
  messageContent = '';
  constructor(private messageService: MessageService) {}
  sendMessage() {
    if (!this.username) return;
    this.messageService
      .sendMessage(this.username, this.messageContent)
      .subscribe({
        next: (response) => {
          this.messages.push(response);
          this.messageForm?.reset();
        },
      });
  }

  onLongPressMessage(id: number) {
    // console.log('delete me, id: ' + id);
    // this.messages?.splice(
    //   this.messages.findIndex((ms) => ms.id === id),
    //   1
    // );

    this.messageService.deleteMessage(id).subscribe({
      next: (_) =>
        this.messages?.splice(
          this.messages.findIndex((ms) => ms.id === id),
          1
        ),
    });
  }
}
