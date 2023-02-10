import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrModule } from 'ngx-toastr';
import { PopupAreYouSureComponent } from './popup-are-you-sure.component';

describe('PopupAreYouSureComponent', () => {
  let component: PopupAreYouSureComponent;
  let fixture: ComponentFixture<PopupAreYouSureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PopupAreYouSureComponent ],
      imports: [ToastrModule.forRoot(), MatDialogModule],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: {} },
        { provide: MatDialogRef, useValue: {} }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PopupAreYouSureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
