import { AccountService } from 'src/app/_services/account.service';
import {
  Directive,
  Input,
  OnInit,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { User } from '../_models/user';

@Directive({
  selector: '[appHasRole]', //*appHasRole="['Admin', 'Moderator']"
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[] = []; //['Admin', 'Moderator']
  user: User = {} as User;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private accountService: AccountService
  ) {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (!user) {
          return;
        }
        this.user = user;
      },
    });
  }
  ngOnInit(): void {
    if (!this.user.roles || this.user == null) {
      this.viewContainerRef.clear();
      return;
    }
    if (this.user.roles.some((r) => this.appHasRole.includes(r))) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }
}
