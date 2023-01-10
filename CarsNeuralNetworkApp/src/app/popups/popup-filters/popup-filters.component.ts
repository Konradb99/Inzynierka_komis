import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { carsConstants } from '../../constants/cars-constants';
import { CarsService } from '../../shared/services/Api/cars.service';

@Component({
  selector: 'app-popup-filters',
  templateUrl: './popup-filters.component.html',
  styleUrls: ['./popup-filters.component.scss'],
})
export class PopupFiltersComponent implements OnInit {
  constructor(
    private router: Router,
    private dialogRef: MatDialogRef<PopupFiltersComponent>,
    private carsApiService: CarsService,
  ) {

    
  }

  years = carsConstants.years;
  prices = carsConstants.prices;
  fuelTypes = carsConstants.fuelTypes;
  bodyTypes = carsConstants.bodyTypes;
  brands = carsConstants.brands;
  gearboxTypes = carsConstants.gearboxTypes;
  driveTypes = carsConstants.driveTypes;
  models: string[] = [];

  loading!: boolean;

  filtersGroup = this.carsApiService.filters ? 
  new FormGroup({
    priceMin: new FormControl(this.carsApiService.filters.priceMin, [Validators.pattern('^[0-9]*$')]),
    priceMax: new FormControl(this.carsApiService.filters.priceMax, [Validators.pattern('^[0-9]*$')]),
    yearMin: new FormControl(this.carsApiService.filters.productionYearMin, [Validators.pattern('^[0-9]*$')]),
    yearMax: new FormControl(this.carsApiService.filters.productionYearMax, [Validators.pattern('^[0-9]*$')]),
    distanceMin: new FormControl(this.carsApiService.filters.distanceMin, [Validators.pattern('^[0-9]*$')]),
    distanceMax: new FormControl(this.carsApiService.filters.distanceMax, [Validators.pattern('^[0-9]*$')]),
    capacityMin: new FormControl(this.carsApiService.filters.capacityMin, [Validators.pattern('^[0-9]*$')]),
    capacityMax: new FormControl(this.carsApiService.filters.capacityMax, [Validators.pattern('^[0-9]*$')]),
    brand: new FormControl(this.carsApiService.filters.brand),
    model: new FormControl(),
    bodyType: new FormControl(this.carsApiService.filters.bodyType),
    driveType: new FormControl(this.carsApiService.filters.driveType),
    gearboxType: new FormControl(this.carsApiService.filters.gearboxType),
    fuelType: new FormControl(this.carsApiService.filters.fuelType),
  }) : 
  new FormGroup({
    priceMin: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    priceMax: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    yearMin: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    yearMax: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    distanceMin: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    distanceMax: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    capacityMin: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    capacityMax: new FormControl(null, [Validators.pattern('^[0-9]*$')]),
    brand: new FormControl(),
    model: new FormControl(),
    bodyType: new FormControl(),
    driveType: new FormControl(),
    gearboxType: new FormControl(),
    fuelType: new FormControl()})


  changeModelsOnBrandSelect() {
    switch (this.filtersGroup.value.brand) {
      case 'Ford':
        this.models = carsConstants.fordModels;
        break;
      case 'Opel':
        this.models = carsConstants.opelModels;
        break;
      case 'Audi':
        this.models = carsConstants.audiModels;
        break;
      case 'BMW':
        this.models = carsConstants.bmwModels;
        break;
      case 'Peugeot':
        this.models = carsConstants.peugeotModels;
        break;
      default:
        this.models = [];
        break;
    }
  }

  async filterCars() {
    this.carsApiService.carsList = []

    let filters = {
      brand:
        this.filtersGroup.value.brand != null
          ? this.filtersGroup.value.brand
          : null,
      model:
        this.filtersGroup.value.model != null
          ? this.filtersGroup.value.model
          : null,
      bodyType:
        this.filtersGroup.value.bodyType != null
          ? this.filtersGroup.value.bodyType
          : null,
      driveType:
        this.filtersGroup.value.driveType != null
          ? this.filtersGroup.value.driveType
          : null,
      gearboxType:
        this.filtersGroup.value.gearboxType != null
          ? this.filtersGroup.value.gearboxType
          : null,
      fuelType:
        this.filtersGroup.value.fuelType != null
          ? this.filtersGroup.value.fuelType
          : null,
      priceMin:
        this.filtersGroup.value.priceMin != null
          ? this.filtersGroup.value.priceMin
          : null,
      priceMax:
        this.filtersGroup.value.priceMax != null
          ? this.filtersGroup.value.priceMax
          : null,
      distanceMin:
        this.filtersGroup.value.distanceMin != null
          ? this.filtersGroup.value.distanceMin
          : null,
      distanceMax:
        this.filtersGroup.value.distanceMax != null
          ? this.filtersGroup.value.distanceMax
          : null,
      productionYearMin:
        this.filtersGroup.value.yearMin != null
          ? this.filtersGroup.value.yearMin
          : null,
      productionYearMax:
        this.filtersGroup.value.yearMax != null
          ? this.filtersGroup.value.yearMax
          : null,
      capacityMin:
        this.filtersGroup.value.capacityMin != null
          ? this.filtersGroup.value.capacityMin
          : null,
      capacityMax:
        this.filtersGroup.value.capacityMax != null
          ? this.filtersGroup.value.capacityMax
          : null,
    };

    this.carsApiService.carsList = [];

    await this.carsApiService.getFilteredCarsPage(
      filters
    ).then(() => {
      const scrollElement = document.getElementById("cars-list__scroll");
      scrollElement?.scrollTo(0, scrollElement.scrollHeight!);
      this.carsApiService.scrolled = true;
      if (this.carsApiService.pairScrolled) {
        const filler = document.createElement('div');
        filler.classList.add("filler-element");
        filler.id = "filler-element";
        scrollElement?.appendChild(filler);
      }
      else {
        const filler = document.getElementById('filler-element')!;
        scrollElement?.removeChild(filler);
      }
      this.closeDialog();
    });
  }

  closeDialog() {
    this.router.navigate(['home']);
    this.dialogRef.close();
  }

  ngOnInit() {}

}
