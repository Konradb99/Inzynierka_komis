import { Injectable } from '@angular/core';
import sha256 from 'crypto-js/sha256';

@Injectable({
  providedIn: 'root'
})
export class PasswordEncryptionService {

  constructor() { }

  encryptPassword(password: string) {
    const hash = sha256(password)
    console.log(hash);
    console.log(hash.toString())
    return(hash);
  }
}
