import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Deliverer } from '../models/user/deliverer.model';
import { Image } from '../models/user/image.model';
import { Login } from '../models/user/login.model';
import { Register } from '../models/user/register.model';
import { Token } from '../models/user/token.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  login(login: Login): Observable<Token> {
    return this.http.post<Token>(environment.serverURL + '/Users/Login', login);
  }

  logout(): void {
    localStorage.removeItem('token');

    return;
  }

  uploadFile(files: any) {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    return this.http.post<Image>(
      environment.serverURL + '/Users/UploadImage',
      formData
    );
  }

  register(register: Register): Observable<Register> {
    return this.http.post<Register>(
      environment.serverURL + '/Users/Register',
      register
    );
  }

  editProfile(edit: Register): Observable<Register> {
    return this.http.put<Register>(
      environment.serverURL + `/Users/EditProfile`,
      edit
    );
  }

  loadProfile(): Observable<Register> {
    return this.http.get<Register>(environment.serverURL + '/Users/User');
  }

  loadDeliverers(): Observable<Deliverer[]> {
    return this.http.get<Deliverer[]>(
      environment.serverURL + '/Users/MyDeliverers'
    );
  }

  activateDelivererProfile(id: number): Observable<Register> {
    return this.http.get<Register>(
      environment.serverURL + `/Users/ActivateProfile/${id}`
    );
  }

  blockDelivererProfile(id: number): Observable<Register> {
    return this.http.get<Register>(
      environment.serverURL + `/Users/BlockProfile/${id}`
    );
  }
}
