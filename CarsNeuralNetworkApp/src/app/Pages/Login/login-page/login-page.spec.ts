import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ToastrModule } from "ngx-toastr";
import { UserService } from "../../../shared/services/Api/user.service";
import { LoginPageComponent } from "./login-page.component";


describe('Login Page Component', () => {

    let userService: UserService
    let component: LoginPageComponent;
    let fixture: ComponentFixture<LoginPageComponent>;

    beforeEach(async(()=> {
        TestBed.configureTestingModule({
            declarations: [LoginPageComponent],
            imports: [ReactiveFormsModule, FormsModule, ToastrModule.forRoot()],
            providers:[UserService]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LoginPageComponent);
        component = fixture.componentInstance;
        userService = fixture.debugElement.injector.get(UserService)
        fixture.detectChanges();
      });


      it('Should reject call loginUser in UserService', () => {
        const spy = spyOn(userService, 'loginUser').and.callFake(()=> Promise.reject("NO!"))
        component.login();
        expect(spy).not.toHaveBeenCalled();
    });
})