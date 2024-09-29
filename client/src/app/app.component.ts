import { Component } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Stock';
  constructor(private accountService: AccountService) {

  }
  ngOnInit(): void {
    this.loadCurrentUser();
  }
  loadCurrentUser(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.accountService.loadCurrentUser(token).subscribe(
        () => {
          // Success
        },
        (error) => {
          console.error('Error loading current user:', error);
        }
      );
    }
  }
}
