import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Image } from 'src/app/shared/models/user/image.model';
import { Register } from 'src/app/shared/models/user/register.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-edit-profile',
  templateUrl: './user-edit-profile.component.html',
  styleUrls: ['./user-edit-profile.component.css'],
})
export class UserEditProfileComponent implements OnInit {
  user: Register = new Register();
  userImagePath: string = '';

  editProfileForm = new FormGroup({
    firstName: new FormControl(this.user.firstName, Validators.required),
    lastName: new FormControl(this.user.lastName, Validators.required),
    email: new FormControl(this.user.email, [
      Validators.required,
      Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
    ]),
    dateOfBirth: new FormControl('', Validators.required),
  });

  constructor(
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadUserDetails();
  }

  loadUserDetails(): void {
    this.userService.loadProfile().subscribe(
      (data: Register) => {
        this.user = data;
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(error.message);
      }
    );
  }

  uploadFile(files: any): void {
    this.userService.uploadFile(files)?.subscribe(
      (data: Image) => {
        this.userImagePath = data.imagePath;
        this.toastr.success('Uspesno ucitana slika!');

        this.router.navigate(['']);
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Upload error' + error.message);
      }
    );
  }

  onSubmit(): void {
    let dateOfBirthString = this.editProfileForm.controls.dateOfBirth.value!;

    let y = dateOfBirthString.split('-')[0];
    let m = dateOfBirthString.split('-')[1];
    let d = dateOfBirthString.split('-')[2];
    let date = new Date(
      Date.UTC(Number.parseInt(y), Number.parseInt(m) - 1, Number.parseInt(d))
    );

    let editProfileObject = this.user;
    editProfileObject.firstName =
      this.editProfileForm.controls.firstName.value!;
    editProfileObject.lastName = this.editProfileForm.controls.lastName.value!;
    editProfileObject.email = this.editProfileForm.controls.email.value!;
    editProfileObject.dateOfBirth = date;

    this.userService.editProfile(editProfileObject).subscribe(
      (data: Register) => {
        this.toastr.success('Uspesno izmenjen profil');
        this.router.navigate(['']);
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Neuspela izmena profila: ' + error.message);
      }
    );
  }

  dateOfBirthInvalid(dateString: string): boolean {
    let y = dateString.split('-')[0];
    let m = dateString.split('-')[1];
    let d = dateString.split('-')[2];

    let date = new Date(
      Date.UTC(Number.parseInt(y), Number.parseInt(m) - 1, Number.parseInt(d))
    );

    if (date >= new Date(Date.now())) return true;
    else return false;
  }
}
