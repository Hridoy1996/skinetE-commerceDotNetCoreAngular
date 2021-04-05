import { Component, OnInit } from '@angular/core';
import { AsyncValidator, AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerFrom: FormGroup;
  errors: string;

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  this.createRegisterFrom();
  }

  createRegisterFrom(){
    this.registerFrom = this.fb.group({
      userName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators
        .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]  
      ],
      password: [null, [Validators.required]]
    });
  }
  onSubmit(){
    this.accountService.register(this.registerFrom.value).subscribe(response => {
      this.router.navigateByUrl('/shop');
    },
    error => {
      this.errors = error;
    });  
  }

  validateEmailNotTaken(): AsyncValidatorFn{
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if(!control.value){
            return of(null);
          }
          return this.accountService.checkEmailExits(control.value).pipe(
            map(res => {
              return res ? {emailExits: true} : null;
            })
          );
        })
      );
    };
  }


}
