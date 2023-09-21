import { CommonModule } from '@angular/common';
import { MembersService } from './../../_services/members.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryModule, ImageItem } from 'ng-gallery';
import { TimeagoModule } from 'ngx-timeago';
@Component({
  selector: 'app-member-details',
  standalone: true,
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css'],
  imports: [CommonModule, TabsModule, GalleryModule, TimeagoModule],
})
export class MemberDetailsComponent implements OnInit {
  member!: Member;
  images: GalleryModule[] = [];
  constructor(
    private membersService: MembersService,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.loadMember();
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
  loadMember() {
    const username = this.route.snapshot.paramMap.get('username');
    if (!username) return;
    this.membersService.getMember(username).subscribe({
      next: (member) => {
        this.member = member;
        this.getImages();
      },
    });
  }
}
