import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/app/shared/services/Internal/local-storage.service';

@Component({
  selector: 'app-starter-page',
  templateUrl: './starter-page.component.html',
  styleUrls: ['./starter-page.component.scss'],
})
export class StarterPageComponent implements OnInit {
  constructor(
    private router: Router,
    private localStorageService: LocalStorageService
  ) {}

  ngOnInit() {}

  navigateToLogin() {
    this.router.navigate(['login']);
  }

  navigateToPage() {
    this.localStorageService.setRoleToGuest();
    this.router.navigate(['home']);
  }
}
