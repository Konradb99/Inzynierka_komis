import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../../environments/environment';
import { Car } from '../../models/Car';
import { PredictedCars } from '../../models/precictedCars';
import { PredictionArguments } from '../../models/predictionArguments';
import { CurrentCarsListService } from '../Internal/current-cars-list.service';
import { CurrentPredictionTypeService } from '../Internal/current-prediction-type.service';
import { CarsService } from './cars.service';

@Injectable({
  providedIn: 'root',
})
export class KnnService {
  apiUri: string = environment.apiUri;
  baseUri: string = 'KNN/';
  predictedCarsList: Car[] = [];
  prefferedClass: string = '';
  arguments_!: PredictionArguments;

  set arguments(arg : PredictionArguments) {
    this.arguments_ = arg;
  }

  get arguments() {
    return this.arguments_;
  }

  constructor(
    private currentPredictionTypeService: CurrentPredictionTypeService, 
    private currentListService: CurrentCarsListService,
    private carsService: CarsService,
    private toastr: ToastrService
  ) {}

  async getKNN(arg: PredictionArguments) {
    this.arguments=arg;
    this.currentListService.setCurrentList('knn');
    const response = await fetch(
      `${this.apiUri}${this.baseUri}Predict?BodyType=${arg.bodyType}&DriveType=${arg.driveType}&GearboxType=${arg.gearboxType}&FuelType=${arg.fuelType}&Price=${arg.price}&Distance=${arg.distanse}&ProductionYear=${arg.year}&Capacity=${arg.capacity}`
    );
    if(response.status<300 && response.status>=200){
      const predictedCars: PredictedCars = await response.json();
      if(predictedCars.prefferedCars?.length!<1){
        this.toastr.warning('Nie znaleziono odpowiadających pojazdów')
        await this.carsService.refreshPage()
      } else {
      this.predictedCarsList=[];
      predictedCars.prefferedCars.forEach((car: Car) => {
        this.predictedCarsList.push(car);
      });
      this.prefferedClass = predictedCars.prefferedClass;
      }
    } else {
      this.currentListService.setCurrentList('main');
      this.toastr.error('Coś poszło nie tak');
    }
  }

  async getFilteredKNN(arg: PredictionArguments) {
    this.arguments=arg;
    this.currentListService.setCurrentList('knn');
    const response = await fetch(
      `${this.apiUri}${this.baseUri}PredictWithFilters?BodyType=${arg.bodyType}&DriveType=${arg.driveType}&GearboxType=${arg.gearboxType}&FuelType=${arg.fuelType}&Price=${arg.price}&Distance=${arg.distanse}&ProductionYear=${arg.year}&Capacity=${arg.capacity}`
    );
    if(response.status<300 && response.status>=200){
      const predictedCars: PredictedCars = await response.json();
      if(predictedCars.prefferedCars?.length!<1){
        this.toastr.warning('Nie znaleziono odpowiadających pojazdów')
        await this.carsService.refreshPage()
      } else {
      this.predictedCarsList=[];
      predictedCars.prefferedCars.forEach((car: Car) => {
      this.predictedCarsList.push(car);
    });
    this.prefferedClass = predictedCars.prefferedClass;
      }  
  } else {
      this.currentListService.setCurrentList('main');
      this.toastr.error('Coś poszło nie tak');
    }
  }
}
