import { Component, OnInit } from '@angular/core';
import { GasStation } from 'src/app/_models/gs';
import { GsService } from 'src/app/_services/gs.service';

@Component({
  selector: 'app-gs-new',
  templateUrl: './gs-new.component.html',
  styleUrls: ['./gs-new.component.scss']
})
export class GsNewComponent implements OnInit {

  constructor(
    private gsService: GsService,
  ) { }

  ngOnInit(): void {
  }

  createNewGS(info: { name: string, address: string, phoneNumber: number}): void {
    var gs = new GasStation();
    gs.name = info.name;
    gs.address = info.address;
    gs.phoneNumber = info.phoneNumber;
    this.gsService.createGasStation(gs);
    location.replace('/dash');

  }
}
