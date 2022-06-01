import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { TokenStorageService } from './token-storage.service';
import * as shajs from 'sha.js';

const AUTH_API = '/api/auth/token/';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private resStatus: number = 0;

  constructor(
    private http: HttpClient,
    private token: TokenStorageService
    ) { }

  public login(info: { login: string, password: string }): Observable<number> {
    return this.http.post<any>(AUTH_API, info, { observe: 'response' })
      .pipe(
        map(res => {
          if (res.status == 200)
            if(res.body.access_token) {
              this.token.saveToken(res.body.access_token);
              this.token.saveUser(res.body.user_name, res.body.id);
              this.token.saveRole(res.body.role);
              this.resStatus = 200;
            }
            else
            {
              this.resStatus = 401;
            }
          return this.resStatus;
        }),
        catchError(error => {
          return of((error as HttpResponse<any>).status);
        }
        )
      )
  }

  public getUsers(): Observable<any> {
    return this.http.get<any>("/api/auth");
  }

  public newUser(data: {login: string, role: string, pass: string}): void {
    let role = (data.role === "Admin" ? 0 : 1);
    this.http.post<any>("/api/auth/auth/newUser", { 
      login: data.login,
      password: shajs('sha256').update(data.pass).digest('hex'),
      role: role
    }).subscribe();
  }

  public deleteUser(id: number) {
    this.http.delete("/api/auth/auth/deleteUser/" + id).subscribe();
  }

  public changepass(id: number, pass: string) {
    this.http.post<any>('/api/auth/auth/changepass/' + id, {hash: shajs('sha256').update(pass).digest('hex')}).subscribe();
  }
}