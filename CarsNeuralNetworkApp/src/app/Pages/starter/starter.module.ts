import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { StarterPageComponent } from './starter-page/starter-page.component';
import { StarterRoutingModule } from './starter-routing.module';

@NgModule({
  imports: [CommonModule, StarterRoutingModule],
  declarations: [StarterPageComponent],
})
export class StarterModule {}
