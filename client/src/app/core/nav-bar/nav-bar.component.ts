import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  currentUser$!: Observable<IUser | null>;
  constructor(
    private accountService: AccountService ){}
    ngOnInit(): void {
      this.currentUser$ = this.accountService.currentUser$;}
}
