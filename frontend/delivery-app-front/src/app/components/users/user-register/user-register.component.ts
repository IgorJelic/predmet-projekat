import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Image } from 'src/app/shared/models/user/image.model';
import { Register } from 'src/app/shared/models/user/register.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css'],
})
export class UserRegisterComponent implements OnInit {
  user = new Register();
  userImagePath: string = '';

  registrationForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
    confirmPassword: new FormControl('', [Validators.required]),
    email: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
    ]),
    dateOfBirth: new FormControl('', [Validators.required]),
    role: new FormControl('', [Validators.required]),
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

  uploadFile(files: any) {
    this.userService.uploadFile(files)?.subscribe(
      (data: Image) => {
        this.userImagePath = data.imagePath;
        this.toastr.success('Uspesno ucitana slika!');
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Upload error' + error.message);
      }
    );
  }

  onSubmit() {
    let dateString = this.registrationForm.controls['dateOfBirth'].value || '';

    if (
      this.dateOfBirthInvalid() ||
      this.confirmPasswordInvalid() ||
      this.registrationForm.invalid ||
      this.userImagePath === ''
    ) {
      if (this.userImagePath === '') {
        this.toastr.error('Dodajte sliku profila!');
      }
      this.toastr.error('Invalid registration form.', 'Registration failed.');
      return;
    } else {
      let y = dateString.split('-')[0];
      let m = dateString.split('-')[1];
      let d = dateString.split('-')[2];

      let date = new Date(
        Date.UTC(Number.parseInt(y), Number.parseInt(m) - 1, Number.parseInt(d))
      );

      let register = new Register();

      register.firstName = this.registrationForm.controls.firstName.value!;
      register.lastName = this.registrationForm.controls.lastName.value!;
      register.email = this.registrationForm.controls.email.value!;
      register.password = this.registrationForm.controls.password.value!;
      register.dateOfBirth = date!;
      register.role = this.registrationForm.controls.role.value!;
      register.imagePath = this.userImagePath!;

      this.userService.register(register).subscribe(
        (data: Register) => {
          // console.warn(JSON.stringify(data));
          this.toastr.success('Uspesna registracija!');
          this.router.navigateByUrl('/user/login');
        },
        (error) => {
          this.toastr.error(`${error.message}`, 'Registration Failed');
        }
      );
    }
  }

  // ! VALIDATORS

  dateOfBirthInvalid(): boolean {
    let dateOfBirth = this.registrationForm.controls['dateOfBirth'].value!;

    let y = dateOfBirth.split('-')[0];
    let m = dateOfBirth.split('-')[1];
    let d = dateOfBirth.split('-')[2];

    let date = new Date(
      Date.UTC(Number.parseInt(y), Number.parseInt(m) - 1, Number.parseInt(d))
    );

    if (date >= new Date(Date.now())) {
      return true;
    } else {
      return false;
    }
  }

  confirmPasswordInvalid(): boolean {
    return (
      this.registrationForm.controls['confirmPassword'].value !==
      this.registrationForm.controls['password'].value
    );
  }

  parseDate(dateString: string): Date {
    if (dateString) {
      return new Date(dateString);
    }
    return new Date();
  }
}
