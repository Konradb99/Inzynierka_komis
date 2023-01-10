import { Injectable } from '@angular/core';
import { LocalStorageConstants } from 'src/app/constants/local-storage-constants';
import { PossibleRoles } from 'src/app/constants/possible-roles';
import { User } from 'src/app/shared/models/User';

@Injectable({
  providedIn: 'root',
})
export class CurrentUserService {
  currentUser: User = new User();
  constructor() {}

  checkIsGuest() {
    const currentRole = window.localStorage.getItem(
      LocalStorageConstants.CarsNeuralRole
    );
    return currentRole === PossibleRoles.guest;
  }

  checkIsEmployee(): boolean {
    const currentRole = window.localStorage.getItem(
      LocalStorageConstants.CarsNeuralRole
    );
    return currentRole === PossibleRoles.employee;
  }
}
