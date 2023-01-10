import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Car } from 'src/app/shared/models/Car';
import { environment } from 'src/environments/environment';
import { CarFilter } from '../../models/carFilter';
import { CarPage } from '../../models/carPage';
import { CurrentCarsListService } from '../Internal/current-cars-list.service';

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  apiUri: string = environment.apiUri;
  baseUri: string = 'Cars/';
  carsList: Car[] = [];
  pageNumber: number = 0;
  filters_!: CarFilter;
  scrolled: boolean = false;
  pairScrolled: boolean = true;
  scrollPageDifference: boolean = true;
  differenceAfterEmptyPage: boolean = true;

  set filters(filters: CarFilter) {
    this.filters_ = filters;
  }

  get filters() {
    return this.filters_;
  }

  constructor(private currentListService: CurrentCarsListService, private toastr: ToastrService,) {}

  async refreshPage() {
    this.carsList = [];
    this.currentListService.setCurrentList('main');
    this.currentListService.setCurrentPage(0);
    await this.getCarsPage().then(() => {
      const scrollElement = document.getElementById("cars-list__scroll");
      scrollElement?.scrollTo(0, scrollElement.scrollHeight!);
      setTimeout(function() {
        scrollElement?.scrollTo(0, 0);
      }, 50)});
  }

  async getCarsPage() {
    this.currentListService.currentPageForMain===1?this.carsList=[] : ''
    this.currentListService.setCurrentList('main');
    const response = await fetch(
      `${this.apiUri}${this.baseUri}GetByPage?pageNumber=${this.currentListService.currentPageForMain}`
    )
    if(response.status<300 && response.status>=200){
      this.currentListService.currentPageForMain===0? this.carsList =[] :''
      const carsPage: CarPage = await response.json();
      if(carsPage.data?.length!<1){
        this.toastr.warning('Nie znaleziono odpowiadających pojazdów')
        await this.refreshPage()
      } else {
      carsPage.data?.forEach((car: Car) => {
        this.carsList.push(car);
      });
      this.currentListService.setCurrentPage(carsPage.pageNumber ?? 0);
      }
    } else {
      this.toastr.error('Wystąpił problem z załadowaniem listy pojazdów')
    }
  }

  async getFilteredCarsPage(carfilter: CarFilter) {
    if (this.filters !== carfilter ) {
      this.currentListService.setCurrentPage(0)
    }  
    this.filters = carfilter;
    this.currentListService.setCurrentList('filtered');
    this.currentListService.currentPageForFilterd===1? this.carsList = [] : ''
    const response = await fetch(
      `${this.apiUri}${this.baseUri}GetFilteredCars?pageNumber=${this.currentListService.currentPageForFilterd}&Brand=${carfilter.brand}&Model=${carfilter.model}&BodyType=${carfilter.bodyType}&DriveType=${carfilter.driveType}&GearboxType=${carfilter.gearboxType}&FuelType=${carfilter.fuelType}&PriceMin=${carfilter.priceMin}&PriceMax=${carfilter.priceMax}&DistanceMin=${carfilter.distanceMin}&DistanceMax=${carfilter.distanceMax}&ProductionYearMin=${carfilter.productionYearMin}&ProductionYearMax=${carfilter.productionYearMax}&CapacityMin=${carfilter.capacityMin}&CapacityMax=${carfilter.capacityMax}`
    );
    if(response.status<300 && response.status>=200){
    this.currentListService.currentPageForFilterd===0? this.carsList = [] : ''
    const carsPage: CarPage = await response.json();
    if(carsPage.data?.length!<1){
      this.toastr.warning('Nie znaleziono odpowiadających pojazdów')
      await this.refreshPage()
    } else {
    carsPage.data?.forEach((car: Car) => {
      this.carsList.push(car);
    });
    this.currentListService.setCurrentPage(carsPage.pageNumber ?? 0);
    }  
  } else {
      this.toastr.error('Wystąpił problem z załadowaniem listy pojazdów');
    }
  }

  async getCarsCounter() {
    const response = await fetch(`${this.apiUri}${this.baseUri}GetCounter`);
    const carsCount: number = await response.json();
    return carsCount;
  }

  async insertCar(carToInsert: Car) {
    const response = await fetch(`${environment.apiUri}Cars/InsertCar`, {
      method: 'POST',
      body: JSON.stringify(carToInsert),
      headers: { 'Content-Type': 'application/json' },
    });
    if(response.status<300 && response.status>=200){
      this.toastr.success('Dodano pojazd do bazy');
    } else {
      this.toastr.error('Coś poszło nie tak');
    }
  }

  async soldCar(carId:number){
    const response = await fetch(`${environment.apiUri}Cars/SellCar`, {
      method: 'PUT',
      body: JSON.stringify(carId),
      headers: { 'Content-Type': 'application/json' },
    });
    if(response.status<300 && response.status>=200){
      this.toastr.success('Pojazd został sprzedany. Odśwież stronę by zaaktualizować listę.');
    } else {
      this.toastr.error('Coś poszło nie tak');
    }
  }

  async deleteCar(carId:number){
    const response = await fetch(`${environment.apiUri}Cars/RemoveCar`, {
      method: 'DELETE',
      body: JSON.stringify(carId),
      headers: { 'Content-Type': 'application/json' },
    });
    if(response.status<300 && response.status>=200){
      this.toastr.success('Pojazd usunięty z bazy. Odśwież stronę by zaaktualizować listę.');
    } else {
      this.toastr.error('Coś poszło nie tak');
    }
  }
}
