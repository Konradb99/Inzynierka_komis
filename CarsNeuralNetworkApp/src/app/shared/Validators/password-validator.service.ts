import { AbstractControl, ValidatorFn } from '@angular/forms';
export default class PasswordValidatorService {
  static match(passwordControl: string, repeatPasswordControl: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const psasword = controls.get(passwordControl);
      const repeatPassword = controls.get(repeatPasswordControl);
      if (psasword?.value !== repeatPassword?.value) {
        return { matching: true };
      } else {
        return null;
      }
    };
  }
}
