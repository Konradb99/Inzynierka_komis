import { Injectable } from '@angular/core';
import { predictionConstants } from '../../../constants/prediction-constants';

@Injectable({
  providedIn: 'root'
})
export class CurrentPredictionTypeService {
  choosenPredictionType:string=predictionConstants.defaultPredictionType

  constructor() { }

  changePredictionType(type:string){
    this.choosenPredictionType = type!=undefined? type:predictionConstants.defaultPredictionType;
  }

  getPredictionType(){
    return this.choosenPredictionType;
  }
}
