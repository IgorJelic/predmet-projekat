import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css'],
})
export class UserHomeComponent implements OnInit {
  userRole: string = '';
  userId: number = 0;
  userActivated: string = '';
  userName: string = '';

  serverImgPath: string = '';
  userImgPath: string = '';

  constructor(
    private router: Router,
    private userService: UserService,
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

    this.userRole = decodedJwtData.role;
    this.userId = decodedJwtData.id;
    this.userActivated = decodedJwtData.status;
    this.userName = decodedJwtData.name;
    this.userImgPath = decodedJwtData.img;
  }

  isDeliverer(): boolean {
    if (this.userRole == 'deliverer') return true;
    else return false;
  }

  logout() {
    this.userService.logout();
    this.toastr.info('Logged out!');
    this.router.navigateByUrl('/user/login');
  }
}
