import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GasStation } from '../_models/gs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FuelData} from '../_models/fueltabledata';

@Injectable({
  providedIn: 'root'
})
export class GsService {

  private gsUrl = '/api/gasstation/';
  private gsUrlAuth = '/api/gasstation/auth/';

  private gasStations: Observable<GasStation[]>;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
  ) {
    this.gasStations = this.http.get<GasStation[]>(this.gsUrl);
    console.log(this.gasStations);
  }

  getGasStationData(): Observable<GasStation[]> {
    return this.http.get<GasStation[]>(this.gsUrl);
  }

  getGasStationById(id: number): Observable<GasStation> {
    return this.http.get<GasStation>(this.gsUrl + id);
  }

  deleteGasStationById(id: number): void {
    this.http.delete(this.gsUrlAuth + "delete/" + id)
    .subscribe((s) => {
      console.log(s);
    });
  }

  createGasStation(gs: GasStation):void {
    this.http.post(this.gsUrlAuth + 'post/', gs).subscribe();
  }

  editGasStation(id:number, gs: GasStation): void {
    this.http.put(this.gsUrlAuth + 'edit/' + id, 
    { 
      name: gs.name,
      address: gs.address,
      phoneNumber: gs.phoneNumber
    }
    ).subscribe();
  }

  getFuelTable(): Observable<FuelData[]> {
    return this.http.get<FuelData[]>(this.gsUrlAuth + 'fuelsOnlyWithPrice');
  }
}
