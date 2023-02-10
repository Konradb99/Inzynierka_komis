import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { carsConstants } from '../../constants/cars-constants';
import { Car } from '../../shared/models/Car';
import { CarsService } from '../../shared/services/Api/cars.service';

@Component({
  selector: 'app-popup-add-car',
  templateUrl: './popup-add-car.component.html',
  styleUrls: ['./popup-add-car.component.scss'],
})
export class PopupAddCarComponent implements OnInit {
  years = carsConstants.years;
  prices = carsConstants.prices;
  fuelTypes = carsConstants.fuelTypes;
  bodyTypes = carsConstants.bodyTypes;
  brands = carsConstants.brands;
  gearboxTypes = carsConstants.gearboxTypes;
  driveTypes = carsConstants.driveTypes;
  models: string[] = [];

  @Output() addedCarEvent = new EventEmitter();

  constructor(
    private dialogRef: MatDialogRef<PopupAddCarComponent>,
    private carsApiService: CarsService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  addCarGroup = new FormGroup({
    price: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]*$'),
    ]),
    year: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]*$'),
    ]),
    producer: new FormControl('', Validators.required),
    model: new FormControl('', Validators.required),
    bodyType: new FormControl('', Validators.required),
    distance: new FormControl('', [
      Validators.required
    ]),
    fuelType: new FormControl('', Validators.required),
    gearboxType: new FormControl('', Validators.required),
    driveType: new FormControl('', Validators.required),
    capacity: new FormControl('', [
      Validators.required
    ]),
  });

  changeModelsOnBrandSelect() {
    switch (this.addCarGroup.value.producer) {
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

  addCar() {
    if (Number(this.addCarGroup.value.year) < 1990) {
      this.addCarGroup.value.year = String(1990);
    }

    const carToAdd = new Car(
      0,
      Number(this.addCarGroup.value.capacity),
      Number(this.addCarGroup.value.year),
      Number(this.addCarGroup.value.distance),
      Number(this.addCarGroup.value.price),
      this.addCarGroup.value.producer!,
      this.addCarGroup.value.model!,
      this.addCarGroup.value.bodyType!,
      this.addCarGroup.value.driveType!,
      this.addCarGroup.value.gearboxType!,
      this.addCarGroup.value.fuelType!
    );

    if (!this.addCarGroup.invalid) {
      this.carsApiService
        .insertCar(carToAdd)
        .then(() => {
          this.addCarGroup.reset();
          this.carsApiService.refreshPage();
        })
    } else {
      this.toastr.warning('Błąd w formularzu');
    }
  }

  closeDialog() {
    this.router.navigate(['home']);
    this.dialogRef.close();
  }

  ngOnInit() {}
}
