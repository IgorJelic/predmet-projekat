import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerDelivererGuard implements CanActivate {
  constructor(private router: Router, private toastr: ToastrService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (localStorage.getItem('token') != null) {
      let tokenString = localStorage.getItem('token')!;
      let jwtData = tokenString.split('.')[1];
      let decodedJwtJsonData = window.atob(jwtData);
      let decodedJwtData = JSON.parse(decodedJwtJsonData);

      let userRole = decodedJwtData.role;
      let status = decodedJwtData.status;

      if (
        userRole == 'customer' ||
        (userRole == 'deliverer' && status == 'active')
      )
        return true;
    }

    this.toastr.error('Access denied!');
    this.router.navigate(['/user/login']);
    return false;
  }
}
