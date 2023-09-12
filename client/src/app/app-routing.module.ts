import { ListsComponent } from './lists/lists.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    component: HomeComponent,
    canActivate: [authGuard],
    runGuardsAndResolvers: 'always',
    children: [
      {
        path: 'lists',
        component: ListsComponent,
      },
      {
        path: 'members',
        component: MemberListComponent,
      },
      {
        path: 'member/:id',
        component: MemberDetailsComponent,
      },
      {
        path: 'messages',
        component: MessagesComponent,
      },
    ],
  },

  { path: '**', pathMatch: 'full', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
