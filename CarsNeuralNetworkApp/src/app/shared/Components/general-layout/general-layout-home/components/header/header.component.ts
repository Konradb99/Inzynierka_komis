import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/shared/services/Api/user.service';
import { predictionConstants } from '../../../../../../constants/prediction-constants';
import { CurrentPredictionTypeService } from '../../../../../services/Internal/current-prediction-type.service';
import { CurrentUserService } from '../../../../../services/Internal/current-user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  predictionTypes = predictionConstants.predictionTypes;
  isEmployee!: boolean;
  predictionTypesChanges!: Subscription;

  constructor(
    private router: Router,
    private userApiService: UserService,
    private currentUser: CurrentUserService,
    private currentPredictionType: CurrentPredictionTypeService
  ) {
    this.isEmployee = this.currentUser.checkIsEmployee();
  }

  ngOnInit() {
    this.onChanges();
  }

  predictionControl = new FormGroup({
    type: new FormControl(''),
  });

  onChanges() {
    this.predictionTypesChanges = this.predictionControl.valueChanges.subscribe(
      (value) => {
        this.currentPredictionType.changePredictionType(value.type!);
      }
    );
  }

  logout() {
    this.userApiService.logout();
    this.router.navigate(['']);
  }

  ngOnDestroy() {
    this.predictionTypesChanges.unsubscribe();
  }
}
