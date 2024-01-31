import { Component, OnInit } from '@angular/core';
import { Message } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { MessageService } from '../_services/message.service';
import {
  faEnvelope,
  faEnvelopeOpen,
  faPaperPlane,
  faTrashCan,
} from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css'],
})
export class MessagesComponent implements OnInit {
  loading = false;

  messages?: Message[];
  pagination?: Pagination;
  label = 'Unread'; // 'Inbox'
  pageNumber = 1;
  pageSize = 5;

  faEnvelopeOpen = faEnvelopeOpen;
  faEnvelope = faEnvelope;
  faPaperPlane = faPaperPlane;
  faTrashcan = faTrashCan;

  constructor(private messageService: MessageService) {}
  ngOnInit(): void {
    this.loadMessage();
  }
  loadMessage() {
    this.loading = true;
    this.messageService
      .getMessages(this.pageNumber, this.pageSize, this.label)
      .subscribe({
        next: (response) => {
          this.messages = response.result;
          this.pagination = response.pagination;
          this.loading = false;
        },
      });
  }
  pageChanged(event: any) {
    if (this.pageNumber === event.page) return;
    this.pageNumber = event.page;
    this.loadMessage();
  }
  deleteMessage(id: number) {
    this.messageService.deleteMessage(id).subscribe({
      next: (_) =>
        this.messages?.splice(
          this.messages.findIndex((ms) => ms.id === id),
          1
        ),
    });
  }
}
