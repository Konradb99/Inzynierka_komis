import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { RouterModule } from '@angular/router';
import { PopupAddCarComponent } from '../../../popups/popup-add-car/popup-add-car.component';
import { PopupAssistantComponent } from '../../../popups/popup-assistant/popup-assistant.component';
import { PopupFiltersComponent } from '../../../popups/popup-filters/popup-filters.component';
import { FooterComponent } from './general-layout-home/components/footer/footer.component';
import { HeaderComponent } from './general-layout-home/components/header/header.component';
import { NavigationBarComponent } from './general-layout-home/components/navigation-bar/navigation-bar.component';
import { GeneralLayoutComponent } from './general-layout-home/general-layout.component';
import { GeneralLayoutRoutingModule } from './general-layout-routing.module';

@NgModule({
  declarations: [
    GeneralLayoutComponent,
    NavigationBarComponent,
    HeaderComponent,
    PopupAssistantComponent,
    FooterComponent,
    PopupAddCarComponent,
    PopupFiltersComponent,
  ],
  imports: [
    CommonModule,
    GeneralLayoutRoutingModule,
    RouterModule,
    HttpClientModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatSelectModule,
  ],
})
export class GeneralLayoutModule {}
