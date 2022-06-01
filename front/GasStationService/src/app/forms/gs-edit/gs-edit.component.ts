import { Component, OnInit } from '@angular/core';
import { GasStation } from 'src/app/_models/gs';
import { GsService } from 'src/app/_services/gs.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-gs-edit',
  templateUrl: './gs-edit.component.html',
  styleUrls: ['./gs-edit.component.scss']
})
export class GsEditComponent implements OnInit {

  gasStation: GasStation = new GasStation();

  constructor(
    private gs: GsService,
    private route: ActivatedRoute,
    private _location: Location
  ) { }

  ngOnInit(): void {
    this.getGasStation();
  }

  getGasStation(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.gs.getGasStationById(id)
      .subscribe(gs => this.gasStation = gs);
  }

  editGS(info: { name: string, address: string, phoneNumber: number }): void {
    var gs = new GasStation();
    gs.name = info.name;
    gs.address = info.address;
    gs.phoneNumber = info.phoneNumber;
    this.gs.editGasStation(this.gasStation.id, gs);
    location.replace("/gsdetail/" + this.gasStation.id);

  }
}
