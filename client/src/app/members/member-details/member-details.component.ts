import { CommonModule } from '@angular/common';
import { MembersService } from './../../_services/members.service';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { TabDirective, TabsModule, TabsetComponent } from 'ngx-bootstrap/tabs';
import { GalleryModule, ImageItem } from 'ng-gallery';
import { TimeagoModule } from 'ngx-timeago';
import { MemberMessagesComponent } from '../member-messages/member-messages.component';
import { MessageService } from 'src/app/_services/message.service';
import { Message } from 'src/app/_models/message';

@Component({
  selector: 'app-member-details',
  standalone: true,
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css'],
  imports: [
    CommonModule,
    TabsModule,
    GalleryModule,
    TimeagoModule,
    MemberMessagesComponent,
  ],
})
export class MemberDetailsComponent implements OnInit, AfterViewInit {
  @ViewChild('memberTabs', { static: true }) memberTabs?: TabsetComponent;
  activeTab?: TabDirective;
  member: Member = {} as Member;
  images: GalleryModule[] = [];
  messages: Message[] = [];
  constructor(
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {}
  ngAfterViewInit(): void {}
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages') {
      this.loadMessages();
    }
  }

  onSelectedTab(heading: string) {
    if (!this.memberTabs) return;
    this.memberTabs.tabs.find((tab) => tab.heading === heading)!.active = true;
  }
  loadMessages() {
    if (this.member) {
      this.messageService.getMessageThread(this.member.userName).subscribe({
        next: (messages) => {
          this.messages = messages;
        },
      });
    }
  }
  ngOnInit(): void {
    //get the member from the route resolver
    this.route.data.subscribe({
      next: (data) => {
        this.member = data['member'];
      },
    });
    this.route.queryParams.subscribe({
      next: (params) => {
        params['tab'] && this.onSelectedTab(params['tab']);
      },
    });
    this.getImages();
  }
  getImages() {
    if (!this.member) return;

    for (const photo of this.member?.photos) {
      this.images.push(
        new ImageItem({
          src: photo.url,
          thumb: photo.url,
        })
      );
    }
  }
}
