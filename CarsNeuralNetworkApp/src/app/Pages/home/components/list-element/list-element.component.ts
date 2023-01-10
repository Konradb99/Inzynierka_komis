import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Car } from 'src/app/shared/models/Car';
import { PopupAreYouSureComponent } from '../../../../popups/popup-are-you-sure/popup-are-you-sure.component';
import { CurrentUserService } from '../../../../shared/services/Internal/current-user.service';

@Component({
  selector: 'app-list-element',
  templateUrl: './list-element.component.html',
  styleUrls: ['./list-element.component.scss'],
})
export class ListElementComponent implements OnInit {
  @Input() car = new Car();
  car_photo = '';
  isUserGuest = this.currentUserService.checkIsGuest();

  constructor(private currentUserService: CurrentUserService, private dialog: MatDialog) {}

  ngOnInit() {
    this.setCarPhotos();
  }

  async soldCar(car:Car){
    this.dialog.open(PopupAreYouSureComponent,{
      width: '300px',
      height: '200px',
      backdropClass: 'popup-backdrop',
      data:{action:'sell', id:car.id}
    })
    //await this.carsService.soldCar(car.id)
  }

  async removeCar(car:Car){
    this.dialog.open(PopupAreYouSureComponent,{
      width: '300px',
      height: '200px',
      backdropClass: 'popup-backdrop',
      data:{action:'remove', id:car.id}
    })
    //await this.carsService.deleteCar(car.id)
  }

  private setCarPhotos() {
    switch (this.car.bodyType) {
      case 'Sedan':
        this.car_photo = '../../../../assets/Cars/sedan.jpg';
        break;
      case 'Kompakt':
        this.car_photo = '../../../../assets/Cars/hatchback.jpg';
        break;
      case 'Suv':
        this.car_photo = '../../../../assets/Cars/suv.jpg';
        break;
      case 'Kombi':
        this.car_photo = '../../../../assets/Cars/kombi.jpg';
        break;
      case 'Coupe':
        this.car_photo = '../../../../assets/Cars/coupe.jpg';
        break;
      case 'Kabriolet':
        this.car_photo = '../../../../assets/Cars/cabrio.jpg';
        break;
      case 'Miejskie':
        this.car_photo = '../../../../assets/Cars/miejskie.jpg';
        break;
      case 'Minivan':
        this.car_photo = '../../../../assets/Cars/minivan.jpg';
        break;
    }
  }
}
