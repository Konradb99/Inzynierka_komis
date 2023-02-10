export class Car {
  brand: string;
  model: string;
  bodyType: string;
  driveType: string;
  gearboxType: string;
  fuelType: string;
  price: number;
  distance: number;
  productionYear: number;
  capacity: number;
  isSold?: boolean;
  id: number;
  constructor();
  constructor(
    id: number,
    capacity: number,
    productionYear: number,
    distance: number,
    price: number,
    brand: string,
    model: string,
    bodyType: string,
    driveType: string,
    gearboxType: string,
    fuelType: string,
    isSold?: boolean
  );
  constructor(
    id?: number,
    capacity?: number,
    productionYear?: number,
    distance?: number,
    price?: number,
    brand?: string,
    model?: string,
    bodyType?: string,
    driveType?: string,
    gearboxType?: string,
    fuelType?: string,
    isSold?: boolean
  ) {
    this.id = id ?? 0;
    this.capacity = capacity ?? 0;
    this.productionYear = productionYear ?? 0;
    this.distance = distance ?? 0;
    this.price = price ?? 0;
    this.brand = brand ?? '';
    this.model = model ?? '';
    this.bodyType = bodyType ?? '';
    this.driveType = driveType ?? '';
    this.gearboxType = gearboxType ?? '';
    this.fuelType = fuelType ?? '';
    this.isSold = isSold ?? false;
  }
}
