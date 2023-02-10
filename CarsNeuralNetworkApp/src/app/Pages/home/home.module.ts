import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule } from '@angular/material/dialog';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { PopupAreYouSureComponent } from '../../popups/popup-are-you-sure/popup-are-you-sure.component';
import { CarsListComponent } from './components/cars-list/cars-list.component';
import { ListElementComponent } from './components/list-element/list-element.component';
import { HomePageComponent } from './home-page/home-page.component';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
  declarations: [HomePageComponent, CarsListComponent, ListElementComponent, PopupAreYouSureComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    InfiniteScrollModule,
    MatAutocompleteModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatDialogModule
  ],
})
export class HomeModule {}
