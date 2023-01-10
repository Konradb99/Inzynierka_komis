import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterRoutingModule } from 'src/app/pages/register/register-routing.module';
import { RegisterPageComponent } from './register-page/register-page.component';

@NgModule({
  imports: [CommonModule, RegisterRoutingModule, ReactiveFormsModule],
  declarations: [RegisterPageComponent],
})
export class RegisterModule {}
