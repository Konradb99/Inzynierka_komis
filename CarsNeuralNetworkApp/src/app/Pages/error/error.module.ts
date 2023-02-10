import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ErrorPageComponent } from './error-page/error-page.component';
import { ErrorRoutingModule } from './error-routing.module';

@NgModule({
  imports: [CommonModule, ErrorRoutingModule],
  declarations: [ErrorPageComponent],
})
export class ErrorModule {}
