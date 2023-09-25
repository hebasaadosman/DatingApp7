import { MessageService } from 'src/app/_services/message.service';
import { Message } from './../../_models/message';
import { Component, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeagoModule } from 'ngx-timeago';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  standalone: true,
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css'],
  imports: [CommonModule, TimeagoModule, FormsModule],
})
export class MemberMessagesComponent {
  @ViewChild('messageForm') messageForm?: NgForm;
  @Input() username: string = '';

  messageContent: string = '';

  constructor(public messageService: MessageService) {}
  ngOnInit() {}
  sendMessage() {
    if (!this.username) return;
    this.messageService
      .sendMessage(this.username!, this.messageContent)

      .then(() => {
        this.messageForm?.reset();
      });
  }
}
