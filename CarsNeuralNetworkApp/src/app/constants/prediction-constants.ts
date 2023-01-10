import { PredictionArguments } from "../shared/models/predictionArguments";

export class predictionConstants {
  public static readonly predictionTypes = [
    'KNN',
    'KNN Z FILTROWANIEM',
    'SIECI NEURONOWE Z KNN'
  ];
  public static readonly defaultPredictionType = 'KNN';
  public static readonly defaultPrice = 0;
  public static readonly defaultYear = new Date().getFullYear();
  public static readonly defaultDistance = 0;
  public static readonly deafultCapacity = 1000;

  public static readonly clearArguments:PredictionArguments = {
    price:null,
    year:null,
    distanse:null,
    capacity:null,
    fuelType:null,
    bodyType:null,
    gearboxType:null,
    driveType:null
  }
}
