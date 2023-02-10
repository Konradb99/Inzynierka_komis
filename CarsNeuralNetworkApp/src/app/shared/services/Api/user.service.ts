import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/shared/models/User';
import { CurrentUserService } from 'src/app/shared/services/Internal/current-user.service';
import { environment } from 'src/environments/environment';
import { RegisterUser } from '../../models/registerUserDto';
import { LocalStorageService } from '../Internal/local-storage.service';
import { PasswordEncryptionService } from '../Internal/password-encryption.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiUri: string = environment.apiUri;
  baseUri: string = 'User/';

  constructor(
    private currentUserService: CurrentUserService,
    private localStorageService: LocalStorageService,
    private passwordEncryptionService: PasswordEncryptionService,
    private toastr: ToastrService,
    private router: Router,
  ) {}

  async loginUser(username: string, password: string) {
    const encryptedPassword = this.passwordEncryptionService.encryptPassword(password).toString();
    const response = await fetch(
      `${this.apiUri}${this.baseUri}LoginUser?username=${username}&password=${encryptedPassword}`
    );
    if(response.status<300 && response.status>=200){
      const loggedUser: User = await response.json();
      this.currentUserService.currentUser = loggedUser;
      this.toastr.success('Zalogowano');
      this.localStorageService.setRoleToEmployee();
      this.router.navigate(['home'])
    } else {
      this.toastr.error('Coś poszło nie tak');
    }
  }

  async register(username: string, password: string, repeatPassword: string) {
    const encryptedPassword = this.passwordEncryptionService.encryptPassword(password).toString();
    const encryptedPasswordRepeat = this.passwordEncryptionService.encryptPassword(repeatPassword).toString();
    const registerUser: RegisterUser = new RegisterUser(username, encryptedPassword, encryptedPasswordRepeat)
    const response = await fetch(
      `${this.apiUri}${this.baseUri}RegisterUser`,
      { 
        method: 'POST',
        body: JSON.stringify(registerUser),
        headers: { 'Content-Type': 'application/json' },
      }
    );
    if(response.status<300 && response.status>=200){
      this.toastr.success('Utworzono nowe konto użytkownika')
      this.router.navigate(['login']);
    } else {
      this.toastr.error('Coś poszło nie tak');
    }
  }

  logout() {}
}
