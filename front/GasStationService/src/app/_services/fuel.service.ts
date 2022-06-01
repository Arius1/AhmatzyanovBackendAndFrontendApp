import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Fuel } from '../_models/fuel';

@Injectable({
  providedIn: 'root'
})
export class FuelService {

  private fUrl = "/api/fuel/";
  private fUrlAuth = "/api/fuel/auth/";
  private data: any = [];

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient
  ) { }

  getFuelData(gsid: number): Observable<Fuel[]>{
    return this.http.get<Fuel[]>(this.fUrl + 'withGasStationBranded/' + gsid);
  }

  getFuelById(id:number): Observable<Fuel> {
    return this.http.get<Fuel>(this.fUrl + id);
  }

  deleteFuel(id: number): void {
    this.http.delete(this.fUrlAuth + "delete/" + id).subscribe();
  }

  postFuel(fuel: Fuel): void {
    this.http.post(this.fUrlAuth + 'post/', fuel).subscribe();
  }
  editFuel(fuel: Fuel, id: number): void {
    this.http.put(
      this.fUrlAuth + 'edit/' + id,
      { 
        brand: fuel.brand,
        price: fuel.price,
        value: fuel.value,
        gasStationId: fuel.gasStationId
      }
      ).subscribe();
  }
}
