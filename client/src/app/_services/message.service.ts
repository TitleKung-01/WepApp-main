import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { getPaginationHeaders, getPaginationResult } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMessages(pageNumber: number, pageSize: number, label: string = 'Unread') {
    let httpParams = getPaginationHeaders(pageNumber, pageSize);
    httpParams = httpParams.append('Label', label);

    const url = this.baseUrl + '/messages';

    return getPaginationResult<Message[]>(url, httpParams, this.http);
  }

  getMessagesThread(username: string) {
    const url = this.baseUrl + '/messages/thread/' + username;
    return this.http.get<Message[]>(url);
  }

  sendMessage(recipientUsername: string, content: string) {
    const url = this.baseUrl + '/messages';
    const body = { recipientUsername, content }; //ต้องสะกดตรงกับ CreateMessageDto.cs
    return this.http.post<Message>(url, body);
  }

  deleteMessage(id: number) {
    const url = this.baseUrl + '/messages/' + id;
    return this.http.delete(url);
  }
}
