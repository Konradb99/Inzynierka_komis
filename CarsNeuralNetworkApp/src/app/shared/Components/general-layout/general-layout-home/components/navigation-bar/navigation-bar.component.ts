import { Component, DoCheck, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AppRoutesNavigate } from 'src/app/constants/app-routes-navigate';
import { PageType } from 'src/app/constants/page-type';
import { CarsService } from 'src/app/shared/services/Api/cars.service';
import { UserService } from 'src/app/shared/services/Api/user.service';
import { carsConstants } from '../../../../../../constants/cars-constants';
import { predictionConstants } from '../../../../../../constants/prediction-constants';
import { PopupAddCarComponent } from '../../../../../../popups/popup-add-car/popup-add-car.component';
import { PopupAssistantComponent } from '../../../../../../popups/popup-assistant/popup-assistant.component';
import { PopupFiltersComponent } from '../../../../../../popups/popup-filters/popup-filters.component';
import { KnnService } from '../../../../../services/Api/knn.service';
import { CurrentUserService } from '../../../../../services/Internal/current-user.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.scss'],
})
export class NavigationBarComponent implements OnInit, DoCheck {
  activePages: PageType = 0;
  pageTypes = PageType;
  pagePaths = AppRoutesNavigate;
  isEmployee!: boolean;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    public userApiService: UserService,
    private carsApiService: CarsService,
    private currentUser: CurrentUserService,
    private carsService: CarsService,
    private knnService: KnnService
  ) {
    this.isEmployee = this.currentUser.checkIsEmployee();
  }

  ngOnInit() {}

  ngDoCheck() {
    switch (this.router.url) {
      case '/': {
        this.activePages = PageType.Home;
        break;
      }
    }
  }

  openFiltersDialog() {
    this.dialog.open(PopupFiltersComponent, {
      width: '1500px',
      height: '775px',
      backdropClass: 'popup-backdrop',
      maxWidth: '100vw',
    });
  }

  openAssistantDialog() {
    this.dialog.open(PopupAssistantComponent, {
      width: '1000px',
      height: '700px',
      backdropClass: 'popup-backdrop',
      maxWidth: '100vw',
    });
  }

  openAddCarDialog() {
    this.dialog.open(PopupAddCarComponent, {
      width: '1000px',
      height: '775px',
      backdropClass: 'popup-backdrop',
      maxWidth: '100vw',
    });
  }

  async refreshCarsList() {
    this.carsApiService.filters=carsConstants.clearFilters
    this.knnService.arguments=predictionConstants.clearArguments
    await this.carsApiService.refreshPage();
  }
}
