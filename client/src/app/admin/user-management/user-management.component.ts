import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';
import { RolesModalComponent } from 'src/app/modals/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css'],
})
export class UserManagementComponent implements OnInit {
  users: User[] = [];
  availableRoles: string[] = ['Admin', 'Moderator', 'Member'];
  bsModalRef: BsModalRef<RolesModalComponent> =
    new BsModalRef<RolesModalComponent>();
  constructor(
    private adminService: AdminService,
    private modalService: BsModalService
  ) {}
  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe({
      next: (users: any) => (this.users = users),
    });
  }
  ngOnInit(): void {
    this.getUsersWithRoles();
  }
  updateUserRoles(user: User) {
    this.adminService
      .updateUserRoles(user.username, user.roles)
      .subscribe(() => {
        this.users.forEach((u) => {
          if (u.username == user.username) {
            u.roles = user.roles;
          }
        });
      });
  }
  openRolesModal(user: User) {
    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        username: user.username,
        availableRoles: this.availableRoles,
        userRoles: [...user.roles],
        title: 'Edit Roles',
      },
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.onHidden?.subscribe({
      next: () => {
        const selectedRoles = this.bsModalRef.content?.userRoles;
        if (selectedRoles && !this.arrayEquals(selectedRoles, user.roles)) {
          user.roles = selectedRoles;
          this.updateUserRoles(user);
        }

      },
    });
  }
  private arrayEquals(a: any[], b: any[]) {
  return JSON.stringify(a.sort()) == JSON.stringify(b.sort());
  }

}
