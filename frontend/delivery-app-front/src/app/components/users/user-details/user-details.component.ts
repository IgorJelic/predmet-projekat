import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Register } from 'src/app/shared/models/user/register.model';
import { UserService } from 'src/app/shared/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
})
export class UserDetailsComponent implements OnInit {
  userId: number = 0;
  userActivated: string = '';
  userRole: string = '';
  userImgPath: string = '';

  serverImgPath: string = '';

  user: Register = new Register();

  constructor(
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/user/login');
    }

    this.serverImgPath = environment.serverImagesURL;

    // Citanje claims iz tokena
    let tokenString = localStorage.getItem('token')!;
    let jwtData = tokenString.split('.')[1];
    let decodedJwtJsonData = window.atob(jwtData);
    let decodedJwtData = JSON.parse(decodedJwtJsonData);

    this.userId = decodedJwtData.id;
    this.userActivated = decodedJwtData.status;
    this.userRole = decodedJwtData.role;
    this.userImgPath = decodedJwtData.img;

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

  isDeliverer(): boolean {
    if (this.userRole == 'deliverer') return true;
    else return false;
  }
}
