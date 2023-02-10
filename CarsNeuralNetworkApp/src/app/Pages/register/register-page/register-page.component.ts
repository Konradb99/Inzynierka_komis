import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/services/Api/user.service';
import PasswordValidatorService from '../../../shared/Validators/password-validator.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss'],
})
export class RegisterPageComponent implements OnInit {
  registerFormGroup = new FormGroup(
    {
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      repeatPassword: new FormControl('', Validators.required),
    },
    {
      validators: [
        PasswordValidatorService.match('password', 'repeatPassword'),
      ],
    }
  );

  constructor(
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {}

  async register() {
    const username = this.registerFormGroup.value.username;
    const password = this.registerFormGroup.value.password;
    const repeatPassword = this.registerFormGroup.value.repeatPassword;

    if (username && password && repeatPassword) {
      if (!this.registerFormGroup.invalid) {
        await this.userService
          .register(username, password, repeatPassword)
      } else {
        this.toastr.warning('Błąd w formularzu');
      }
    }
  }
}
