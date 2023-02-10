import { Component, OnInit } from '@angular/core';
import { CarsService } from 'src/app/shared/services/Api/cars.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit {
  constructor(public carsApiService: CarsService) {}

  carsCount!: Promise<number>;

  async ngOnInit() {
    this.carsCount = this.carsApiService.getCarsCounter();
  }
}
