import { Component } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../account.service';
import { timer, switchMap, of, map } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm!: FormGroup;
  errors: string[] = [];  // Initialize as an empty array
  isPasswordFocused: boolean = false; // Flag to track whether password field is focused


  constructor(private fb: FormBuilder, private accountService: AccountService, 
    private router: Router, private toastr: ToastrService) {}

    ngOnInit(): void {
      this.createRegisterForm();
    }

    createRegisterForm() {
      this.registerForm = this.fb.group({
        displayName: [null, [Validators.required]],     
        email: [null, 
          [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
          [this.validateEmailNotTaken()]
        ],
        password: [null, [Validators.required, this.passwordValidator()]],
      });
    }
    

    onSubmit() {
      this.accountService.register(this.registerForm.value).subscribe({
        next: () => {
          this.toastr.success('User created successfully');
          this.router.navigateByUrl('/mainwearhouse');
        },
        error: (error) => {
          this.errors = (error && error.errors) || ['An unexpected error occurred'];
        }
      });
    }

    validateEmailNotTaken(): AsyncValidatorFn {
      return control => {
        return timer(500).pipe(
          switchMap(() => {
            if (!control.value) {
              return of(null);
            }
            return this.accountService.checkEmailExists(control.value).pipe(
              map(res => {
                return res ? { emailExists: true } : null;
              })
            );
          })
        );
      };
    }

    passwordValidator(): ValidatorFn {
      return (control: AbstractControl): ValidationErrors | null => {
        const value: string = control.value || '';
        
        const hasUpperCase = /[A-Z]/.test(value);
        const hasLowerCase = /[a-z]/.test(value);
        const hasNumeric = /[0-9]/.test(value);
        const hasSymbol = /[-!@#$%^&*(),.?":{}|<>]/.test(value);
        const minLength = value.length >= 8;
        
        const isValid = hasUpperCase && hasLowerCase && hasNumeric && hasSymbol && minLength;
        
        return isValid ? null : { 
          invalidPassword: true, 
          missingUppercase: !hasUpperCase, 
          missingLowercase: !hasLowerCase,
          missingNumeric: !hasNumeric,
          missingSymbol: !hasSymbol,
          minLength: !minLength
        };
      };
    }

}
