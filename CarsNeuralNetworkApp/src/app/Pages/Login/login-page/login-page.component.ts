import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/services/Api/user.service';
import { LocalStorageService } from '../../../shared/services/Internal/local-storage.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent implements OnInit {
  loginFormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService,
    private localStorageService: LocalStorageService
  ) {}

  ngOnInit() {}

  async login() {
    const username = this.loginFormGroup.value.username;
    const password = this.loginFormGroup.value.password;
    // TODO: Validate is username or passowrd empty
    if (username && password) {
      await this.userService
        .loginUser(username, password)
    } else {
      this.toastr.warning('Błąd logowania');
    }
  }

  register() {
    this.router.navigate(['register']);
  }
}
