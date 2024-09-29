import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm!: FormGroup; 

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.createLoginForm();
    this.fillSavedCredentials();
  }


  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password: new FormControl('', Validators.required),
      rememberMe: new FormControl(this.isRemembered()) // Initialize rememberMe checkbox based on stored value
    });
  }
  onSubmit() {
    const formValues = this.loginForm.value;
    
    // Save email and password if rememberMe checkbox is checked
    if (formValues.rememberMe) {
      localStorage.setItem('savedEmail', formValues.email);
      localStorage.setItem('savedPassword', formValues.password);
    } else {
      // Clear saved credentials if rememberMe is unchecked
      localStorage.removeItem('savedEmail');
      localStorage.removeItem('savedPassword');
    }

    // Check if the email exists before submitting
    this.checkEmail(() => {
      // Email exists, proceed with login
      this.accountService.login(this.loginForm.value).subscribe(() => {
        console.log("Success")
        const currentUser = this.accountService.getCurrentUserValue();
        this.router.navigateByUrl('/mainwearhouse');
        // Check if currentUser is not null and has the 'Seller' or 'Admin' role
        // if (currentUser && currentUser.roles && (currentUser.roles.includes('Seller') || currentUser.roles.includes('Admin'))) {
        //   this.router.navigateByUrl('/settings');
        // } else {
        //   // For other roles or in case the role is not specified, redirect to home
        //   this.router.navigateByUrl('/');
        // }
    });
   } );
  }
  checkEmail(callback: () => void) {
    const email = this.loginForm.value.email;
    this.accountService.checkEmailExists(email).subscribe(
      (result: any) => {
        if (result === true) {
          // Email exists, call the callback function
          callback();
        } else {
          alert('Email is incorrect, please try again with the correct email.');
        }
      },
      (error) => {
        console.error('Error checking email:', error);
        // Handle error, show an error message, etc.
      }
    );
  }

  isRemembered(): boolean {
    return localStorage.getItem('savedEmail') !== null && localStorage.getItem('savedPassword') !== null;
  }
  fillSavedCredentials() {
    const savedEmail = localStorage.getItem('savedEmail');
    const savedPassword = localStorage.getItem('savedPassword');
    if (savedEmail && savedPassword) {
      this.loginForm.patchValue({
        email: savedEmail,
        password: savedPassword
      });
    }
  }

  
}