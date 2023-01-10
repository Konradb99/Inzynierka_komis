import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';

import { RegisterPageComponent } from './register-page.component';

describe('RegisterPageComponent',()=>{
    let component: RegisterPageComponent;
    let fixture: ComponentFixture<RegisterPageComponent>;

    beforeEach(async()=>{
        TestBed.configureTestingModule({
            declarations: [RegisterPageComponent],
            imports:[ToastrModule.forRoot(), FormsModule, ReactiveFormsModule],
            providers: [ ]
        })
    .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(RegisterPageComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be valid', () => {
        component.registerFormGroup.controls.username.setValue('user');
        component.registerFormGroup.controls.password.setValue('abcde');
        component.registerFormGroup.controls.repeatPassword.setValue('abcde');
        expect(component.registerFormGroup.valid).toBeTrue()
    })

    it('should be invalid', () => {
        component.registerFormGroup.controls.username.setValue('user');
        component.registerFormGroup.controls.password.setValue('abcde');
        component.registerFormGroup.controls.repeatPassword.setValue('abcdE');
        expect(component.registerFormGroup.valid).toBeFalsy()
    })

})