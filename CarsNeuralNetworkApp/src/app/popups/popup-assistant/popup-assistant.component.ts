import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { carsConstants } from '../../constants/cars-constants';
import { predictionConstants } from '../../constants/prediction-constants';
import { PredictionArguments } from '../../shared/models/predictionArguments';
import { KnnService } from '../../shared/services/Api/knn.service';
import { NeuralNetworkService } from '../../shared/services/Api/neural-network.service';
import { CurrentPredictionTypeService } from '../../shared/services/Internal/current-prediction-type.service';

@Component({
  selector: 'app-popup-assistant',
  templateUrl: './popup-assistant.component.html',
  styleUrls: ['./popup-assistant.component.scss'],
})
export class PopupAssistantComponent implements OnInit {
  constructor(
    private router: Router,
    private dialogRef: MatDialogRef<PopupAssistantComponent>,
    private currentPredictionType: CurrentPredictionTypeService,
    private knn: KnnService,
    private neuralNetwork: NeuralNetworkService,
    private toastr: ToastrService
  ) {}

  years = carsConstants.years;
  prices = carsConstants.prices;
  fuelTypes = carsConstants.fuelTypes;
  bodyTypes = carsConstants.bodyTypes;
  gearboxTypes = carsConstants.gearboxTypes;
  driveTypes = carsConstants.driveTypes;

  assistantGroup = this.knn.arguments ?
  new FormGroup({
    price: new FormControl(this.knn.arguments.price, [ Validators.pattern('^[0-9]*$')]),
    year: new FormControl(this.knn.arguments.year, [ Validators.pattern('^[0-9]*$')]),
    distance: new FormControl(this.knn.arguments.distanse, [ Validators.pattern('^[0-9]*$')]),
    capacity: new FormControl(this.knn.arguments.capacity, [ Validators.pattern('^[0-9]*$')]),
    driveType: new FormControl(this.knn.arguments.driveType),
    bodyType: new FormControl(this.knn.arguments.bodyType),
    gearboxType: new FormControl(this.knn.arguments.gearboxType),
    fuelType: new FormControl(this.knn.arguments.fuelType),
  }) : 
  new FormGroup({
    price: new FormControl(0, [ Validators.pattern('^[0-9]*$')]),
    year: new FormControl(2022, [ Validators.pattern('^[0-9]*$')]),
    distance: new FormControl(0, [ Validators.pattern('^[0-9]*$')]),
    capacity: new FormControl(1000, [ Validators.pattern('^[0-9]*$')]),
    driveType: new FormControl(),
    bodyType: new FormControl(),
    gearboxType: new FormControl(),
    fuelType: new FormControl(),
  })


  async predict() {
    let predictionArguments: PredictionArguments = {
      price:
        this.assistantGroup.value.price != null
          ? this.assistantGroup.value.price
          : predictionConstants.defaultPrice,
      year:
        this.assistantGroup.value.year != null
          ? this.assistantGroup.value.year
          : predictionConstants.defaultYear,
      distanse:
        this.assistantGroup.value.distance != null
          ? this.assistantGroup.value.distance
          : predictionConstants.defaultDistance,
      capacity:
        this.assistantGroup.value.capacity != null
          ? this.assistantGroup.value.capacity
          : predictionConstants.deafultCapacity,
      fuelType: this.assistantGroup.value.fuelType,
      bodyType: this.assistantGroup.value.bodyType,
      gearboxType: this.assistantGroup.value.gearboxType,
      driveType: this.assistantGroup.value.driveType,
    };

    switch (this.currentPredictionType.getPredictionType()) {
      case 'KNN': {
        await this.knn.getKNN(predictionArguments).then(() => {
          const scrollElement = document.getElementById("cars-list__scroll");
          scrollElement?.scrollTo(0, scrollElement.scrollHeight!);
          setTimeout(function() {
            scrollElement?.scrollTo(0, 0);
          }, 50);
          this.closeDialog();
        });
        break;
      }
      case 'KNN Z FILTROWANIEM': {
        await this.knn.getFilteredKNN(predictionArguments).then(() => {
          const scrollElement = document.getElementById("cars-list__scroll");
          scrollElement?.scrollTo(0, scrollElement.scrollHeight!);
          setTimeout(function() {
            scrollElement?.scrollTo(0, 0);
          }, 50);
          this.closeDialog();
        });
        break;
      }
      case 'SIECI NEURONOWE Z KNN': {
        await this.neuralNetwork.getNeuralNetworkPrediction(predictionArguments).then(() => {
          const scrollElement = document.getElementById("cars-list__scroll");
          scrollElement?.scrollTo(0, scrollElement.scrollHeight!);
          setTimeout(function() {
            scrollElement?.scrollTo(0, 0);
          }, 50);
          this.closeDialog();
        })
        break;
      }
      default:
        break;
    }
  }

  closeDialog() {
    this.router.navigate(['home']);
    this.dialogRef.close();
  }

  ngOnInit() {}
}
