import { Component, OnInit } from '@angular/core';
import { CarsService } from '../../../shared/services/Api/cars.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent implements OnInit {
  constructor(private carsService: CarsService) {}

  ngOnInit() {
    this.carsService.refreshPage()
  }
}
