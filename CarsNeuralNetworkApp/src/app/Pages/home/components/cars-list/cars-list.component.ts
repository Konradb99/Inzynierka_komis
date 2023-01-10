import { Component, OnInit } from '@angular/core';
import { CarsService } from 'src/app/shared/services/Api/cars.service';
import { Car } from '../../../../shared/models/Car';
import { KnnService } from '../../../../shared/services/Api/knn.service';
import { NeuralNetworkService } from '../../../../shared/services/Api/neural-network.service';
import { CurrentCarsListService } from '../../../../shared/services/Internal/current-cars-list.service';

@Component({
  selector: 'app-cars-list',
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.scss'],
})
export class CarsListComponent implements OnInit {
  constructor(
    public carsApiService: CarsService,
    private currentCarsListService: CurrentCarsListService,
    private knnService: KnnService,
    private neuralNetworkService: NeuralNetworkService
  ) {}
  carsList!: Car[];
  prefferedCar!:string;

  ngOnInit() {
    this.loadNextPage();
  }

  isPrediction() {
    if(this.currentCarsListService.getCurrentList()==='knn' || this.currentCarsListService.getCurrentList()==='mix'){
      return true;
    } else {
      return false;
    }
  }

  infiniteScroll(){
    const scrollElement = document.getElementById("cars-list__scroll");
      if (this.carsApiService.scrollPageDifference) {
        const filler = document.createElement('div');
        filler.classList.add(".cars-list__filler-element");
        filler.id = "filler-element";
        scrollElement?.appendChild(filler);
      }
      else {
        const filler = document.getElementById('filler-element')!;
        scrollElement?.removeChild(filler);
      }
      this.carsApiService.scrolled = false;
  }

  async loadNextPage() {
    const scrollElement = document.getElementById("cars-list__scroll");
    if (this.carsApiService.differenceAfterEmptyPage === true) {
      const filler = document.createElement('div');
      filler.classList.add("filler-element");
      filler.id = "filler-element";
      scrollElement?.appendChild(filler);
      this.carsApiService.differenceAfterEmptyPage = false;
    }
    else {
      const filler = document.getElementById('filler-element')!;
      filler? scrollElement?.removeChild(filler) : ''
      this.carsApiService.differenceAfterEmptyPage = true;
    }
    switch (this.currentCarsListService.getCurrentList()) {
      case 'main': 
        await this.carsApiService
          .getCarsPage()
        this.carsList = this.carsApiService.carsList;
        if (this.currentCarsListService.currentPageForMain-2 === 1) {
          document.getElementById("cars-list__scroll")!.scrollTop = 0;
        }
        this.infiniteScroll()
        break;
      case 'filtered': {
        if (!this.carsApiService.scrolled) {
          await this.carsApiService
            .getFilteredCarsPage(this.carsApiService.filters_)
        }
        this.carsList = this.carsApiService.carsList;
        if (this.currentCarsListService.currentPageForFilterd-1 === 1) {
          document.getElementById("cars-list__scroll")!.scrollTop = 0;
        }
        this.infiniteScroll()
        break;
      }
      case 'knn':{
        this.carsList=this.knnService.predictedCarsList;
        this.prefferedCar=this.knnService.prefferedClass;
        document.getElementById("cars-list__scroll")!.scrollTop = 0;
        this.infiniteScroll()
        break;
      }
      case 'mix':{
        this.carsList=this.neuralNetworkService.predictedCarsList;
        this.prefferedCar=this.neuralNetworkService.prefferedClass;
        document.getElementById("cars-list__scroll")!.scrollTop = 0;
        this.infiniteScroll()
        break;
      }
    }
  }
}
