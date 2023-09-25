import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment.development';
import { User } from '../_models/user';
import { BehaviorSubject, take } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnection?: HubConnection;
  public onlineUsersSource$ = new BehaviorSubject<string[]>([]);
  constructor(private toastr: ToastrService, private router: Router) {}

  createHubConnection(user: User) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => user.token,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((error) => console.log(error));
    this.hubConnection.on('UserIsOnline', (username) => {
      this.onlineUsersSource$.pipe(take(1)).subscribe({
        next: (usernames) => {
          this.onlineUsersSource$.next([...usernames, username]);
        },
      });
    });
    this.hubConnection.on('UserIsOffline', (username) => {
      this.onlineUsersSource$.pipe(take(1)).subscribe({
        next: (usernames) => {
          this.onlineUsersSource$.next([
            ...usernames.filter((x) => x !== username),
          ]);
        },
      });
    });
    this.hubConnection.on('GetOnlineUsers', (usernames: string[]) => {
      this.onlineUsersSource$.next(usernames);
    });
    this.hubConnection.on('NewMessageReceived', ({ username, knownAs }) => {
      this.toastr
        .info(knownAs + ' has sent you a new message!')
        .onTap.pipe(take(1))
        .subscribe({
          next: () => {
            this.router.navigateByUrl('/members/' + username + '?tab=Messages');
            //this.hubConnection?.invoke('MarkMessageAsRead', username);
          },
        });
    });
  }
  stopHubConnection() {
    this.hubConnection?.stop().catch((error) => console.log(error));
  }
  getOnlineUsers() {
    return this.onlineUsersSource$.asObservable();
  }
}
