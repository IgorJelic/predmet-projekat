import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Login } from 'src/app/shared/models/user/login.model';
import { Token } from 'src/app/shared/models/user/token.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css'],
})
export class UserLoginComponent implements OnInit {
  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
    ]),
    password: new FormControl('', Validators.required),
  });

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.toastr.error('Form invalid!');
      return;
    }

    let login: Login = new Login();
    login.email = this.loginForm.controls['email'].value || '';
    login.password = this.loginForm.controls['password'].value || '';

    this.userService.login(login).subscribe(
      (data: Token) => {
        // Citanje claims iz tokena
        let tokenString = data.token;
        let jwtData = tokenString.split('.')[1];
        let decodedJwtJsonData = window.atob(jwtData);
        let decodedJwtData = JSON.parse(decodedJwtJsonData);

        let userRole = decodedJwtData.role;
        // let userId = decodedJwtData.id;
        // let userActivated = decodedJwtData.status;

        localStorage.setItem('token', tokenString);

        this.toastr.success(`${userRole}`, 'Logged in!');
        this.router.navigateByUrl(`/home`);
      },
      (error) => {
        this.toastr.error(
          'Incorrect username or password.',
          'Authentication Failed.'
        );
      }
    );
  }
}
