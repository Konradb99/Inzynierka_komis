import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CarsService } from '../../shared/services/Api/cars.service';

@Component({
  selector: 'app-popup-are-you-sure',
  templateUrl: './popup-are-you-sure.component.html',
  styleUrls: ['./popup-are-you-sure.component.scss']
})
export class PopupAreYouSureComponent implements OnInit {

  constructor(private carsService: CarsService, 
              @Inject(MAT_DIALOG_DATA) private data:any,
              private dialogRef: MatDialogRef<PopupAreYouSureComponent>,) { }

  ngOnInit() {
  }

  async yes(){
    if(this.data.action==='sell'){
      await this.carsService.soldCar(this.data.id).then(()=>this.dialogRef.close())
    } else {
      await this.carsService.deleteCar(this.data.id).then(()=>this.dialogRef.close())
    }
  }

  async no(){
    this.dialogRef.close()
  }

}
