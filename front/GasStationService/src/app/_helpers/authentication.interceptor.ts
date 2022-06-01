import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { SRV_URL } from 'src/app/config';
import { TokenStorageService } from '../_services/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class HTTPInterceptorService implements HttpInterceptor {

  constructor(
    private token: TokenStorageService
  ) { }
  
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let headers = req.headers;
    if (
      req.url.includes("/api/fuel/auth/") || 
      req.url.includes("/api/gasstation/auth/") || 
      req.url.includes("api/auth/auth/")
    )
    {
      const token = this.token.getToken();
      headers = headers.append('Authorization', `Bearer ${token}`);
    }
    const request = req.clone({
      headers,
      url:`${SRV_URL}${req.url}`,
    });
    console.log(request);
    return next.handle(request).pipe(
      tap(
        (event) => {
          if (event instanceof HttpResponse)
            console.log('Server response' + event.status);
        },
        (err) => {
          if (err instanceof HttpErrorResponse) {
            if (err.status == 401)
              console.log('Unauthorized');
          }
        }
      )
    )
  }
}
