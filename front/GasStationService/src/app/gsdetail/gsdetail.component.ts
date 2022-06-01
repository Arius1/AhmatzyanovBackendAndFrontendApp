import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { GsService } from '../_services/gs.service';
import { GasStation } from '../_models/gs';
import { FuelService } from '../_services/fuel.service';
import { Fuel } from '../_models/fuel';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { TokenStorageService } from '../_services/token-storage.service';
import { FuelBranded } from '../_models/fuelBranded';
@Component({
  selector: 'app-gsdetail',
  templateUrl: './gsdetail.component.html',
  styleUrls: ['./gsdetail.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class GsdetailComponent implements OnInit {

  gasStation: GasStation = new GasStation();
  columnsToDisplay: string[] = ['id', 'brand', 'price', 'value'];
  dataSource: Fuel[] = [];
  expandedElement!: string;
  isNewFuelCreation: boolean = false;
  isEditFuel: boolean = false;
  fuelToEdit: Fuel = new Fuel();
  selectedBrand: string = "";

  constructor(
    private gs: GsService,
    private route: ActivatedRoute,
    private location: Location,
    private fuel: FuelService,
    private token: TokenStorageService
  ) { }

  ngOnInit(): void {
    this.getGasStation();
    this.getFuelsData();
  }

  getGasStation(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.gs.getGasStationById(id)
      .subscribe(gs => this.gasStation = gs);
  }

  getFuelsData(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.fuel.getFuelData(id).subscribe(fd => this.dataSource = fd);
  }

  deleteFuel(id: number): void {
    this.fuel.deleteFuel(id);
    location.reload();
  }

  isAdmin(): boolean {
    var role = this.token.getRole();
    var isAdm = (role === 0);
    return isAdm;
  }

  deleteGasStation(): void {
    this.gs.deleteGasStationById(this.gasStation.id);
    location.replace("/dash");
  }

  addFuel(): void {
    this.isEditFuel = false;
    this.isNewFuelCreation = true;
  }

  createNewFuel(info: { brand: string, price: number, value: number}): void {
    var fuel = new Fuel();
    fuel.brand = parseInt(info.brand);
    fuel.price = info.price;
    fuel.value = info.value;
    fuel.gasStationId = this.gasStation.id;
    this.fuel.postFuel(fuel);
    location.reload();
  }

  editFuelFiller(id: number):void {
    this.isEditFuel = true;
    this.isNewFuelCreation = false;
    this.fuel.getFuelById(id).subscribe(
      fd => {
        this.fuelToEdit = fd;
        this.selectedBrand = fd.brand.toString()
      });
  }
  editFuel(info: { brand: string, price: number, value: number}): void {
    this.fuelToEdit.brand = parseInt(info.brand);
    this.fuelToEdit.price = info.price;
    this.fuelToEdit.value = info.value;
    this.fuelToEdit.gasStationId = this.gasStation.id;
    this.fuel.editFuel(this.fuelToEdit, this.fuelToEdit.id);
    location.reload();
  }
  
}
