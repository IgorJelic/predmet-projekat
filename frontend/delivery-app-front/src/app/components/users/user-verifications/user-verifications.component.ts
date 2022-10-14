import { getLocaleDayPeriods } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Deliverer } from 'src/app/shared/models/user/deliverer.model';
import { Register } from 'src/app/shared/models/user/register.model';
import { UserService } from 'src/app/shared/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-verifications',
  templateUrl: './user-verifications.component.html',
  styleUrls: ['./user-verifications.component.css'],
})
export class UserVerificationsComponent implements OnInit {
  deliverers: Deliverer[] = [];
  serverImgPath = '';

  constructor(
    private userService: UserService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.serverImgPath = environment.serverImagesURL;
    this.loadDeliverers();
  }

  loadDeliverers(): void {
    this.userService.loadDeliverers().subscribe(
      (data: Deliverer[]) => {
        this.deliverers = data;
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(error.message);
      }
    );
  }

  activate(id: number) {
    this.userService.activateDelivererProfile(id).subscribe(
      (data: Register) => {
        this.toastr.success(
          `Dostavljac ${data.firstName} ${data.lastName} uspesno aktiviran!`
        );
        this.loadDeliverers();
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Aktivacija profila nije uspela!', error.message);
      }
    );
  }

  block(id: number) {
    this.userService.blockDelivererProfile(id).subscribe(
      (data: Register) => {
        this.toastr.success(
          `Dostavljac ${data.firstName} ${data.lastName} uspesno blokiran!`
        );
        this.loadDeliverers();
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Blokiranje profila nije uspelo!', error.message);
      }
    );
  }
}
