import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GasStation } from '../_models/gs';
import { GsService } from '../_services/gs.service';
import { TokenStorageService } from '../_services/token-storage.service';

@Component({
  selector: 'app-dash',
  templateUrl: './dash.component.html',
  styleUrls: ['./dash.component.scss']
})
export class DashComponent implements OnInit {

  gasStationData: GasStation[] = [];

  constructor(
    private gsService: GsService,
    private token: TokenStorageService
    ) { }

  ngOnInit(): void {
    this.getGasStationData();
  }

  getGasStationData(): void {
    this.gsService.getGasStationData()
      .subscribe(data => this.gasStationData = data);
  }

  isAdmin(): boolean {
    var role = this.token.getRole();
    var isAdm = (role === 0);
    return isAdm;
  }

}
