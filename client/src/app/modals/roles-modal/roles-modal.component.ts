import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css'],
})
export class RolesModalComponent implements OnInit {
  username = '';
  availableRoles: string[] = [];
  userRoles: string[] = [];

  constructor(public bsModalRef: BsModalRef) {}
  ngOnInit(): void {}
 
  updateCheckedRoles(role: string) {
    const index = this.userRoles.indexOf(role);
    if (index > -1) {
      this.userRoles.splice(index, 1);
    } else {
      this.userRoles.push(role);
    }
  }
}
