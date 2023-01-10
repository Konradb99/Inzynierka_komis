export interface CarFilter {
  brand: string | null;
  model: string | null;
  bodyType: string | null;
  driveType: string | null;
  gearboxType: string | null;
  fuelType: string | null;
  priceMin: number | null;
  priceMax: number | null;
  distanceMin: number | null;
  distanceMax: number | null;
  productionYearMin: number | null;
  productionYearMax: number | null;
  capacityMin: number | null;
  capacityMax: number | null;
}
