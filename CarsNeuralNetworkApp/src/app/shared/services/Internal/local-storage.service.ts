import { Injectable } from '@angular/core';
import { LocalStorageConstants } from 'src/app/constants/local-storage-constants';
import { PossibleRoles } from 'src/app/constants/possible-roles';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  constructor() {}

  setRoleToGuest() {
    window.localStorage.setItem(
      LocalStorageConstants.CarsNeuralRole,
      PossibleRoles.guest
    );
  }

  setRoleToEmployee() {
    window.localStorage.setItem(
      LocalStorageConstants.CarsNeuralRole,
      PossibleRoles.employee
    );
  }
}
