import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginPageComponent } from './login-page/login-page.component';
import { LoginRoutingModule } from './login-routing.module';

@NgModule({
  imports: [CommonModule, LoginRoutingModule, ReactiveFormsModule],
  declarations: [LoginPageComponent],
})
export class LoginModule {}
