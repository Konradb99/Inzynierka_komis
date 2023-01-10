import { CarFilter } from "../shared/models/carFilter";

export class carsConstants {
  public static readonly years = [
    1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001,
    2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013,
    2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022,
  ];

  public static readonly prices = [
    1000, 2000, 3000, 4000, 5000, 10000, 15000, 20000, 30000, 40000, 50000,
    100000,
  ];

  public static readonly bodyTypes = [
    'Sedan',
    'Kompakt',
    'Suv',
    'Kombi',
    'Coupe',
    'Kabriolet',
    'Miejskie',
    'Minivan',
  ];

  public static readonly fuelTypes = [
    'Diesel',
    'Benzyna',
    'Hybryda',
    'Elektryczny',
    'LPG',
  ];

  public static readonly brands = ['Audi', 'Ford', 'Opel', 'BMW', 'Peugeot'];

  public static readonly gearboxTypes = ['Manualna', 'Automatyczna'];

  public static readonly driveTypes = [
    'Na przednie koła',
    'Na tylnie koła',
    'Na wszystkie koła',
  ];

  public static readonly audiModels = ['A3', 'A4', 'A5', 'E-Tron', 'Q5'];

  public static readonly fordModels = [
    'Focus',
    'Mondeo',
    'Mustang',
    'Kuga',
    'S-Max',
  ];

    public static readonly opelModels = ["Astra", "Insignia", "Mokka", "Zafira", "Corsa"];

  public static readonly bmwModels = [
    'Seria 3',
    'Seria 5',
    'Seria 8',
    'X3',
    'X5',
  ];

  public static readonly peugeotModels = ['208', '308', '3008', '508', '5008'];

  public static readonly clearFilters:CarFilter={
  brand: null,
  model:null,
  bodyType:null,
  driveType: null,
  gearboxType: null,
  fuelType:null,
  priceMin:null,
  priceMax:null,
  distanceMin: null,
  distanceMax: null,
  productionYearMin:null,
  productionYearMax:  null,
  capacityMin:null,
  capacityMax: null,
  }
}
