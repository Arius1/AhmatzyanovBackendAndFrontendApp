import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FuelData} from '../_models/fueltabledata';
import { GsService } from '../_services/gs.service'

@Component({
  selector: 'app-fuel-table',
  templateUrl: './fuel-table.component.html',
  styleUrls: ['./fuel-table.component.scss']
})
export class FuelTableComponent implements OnInit {
  displayedColumns: string[] = ['gasStationName', 'fuel', 'price'];
  dataSource: MatTableDataSource<FuelData>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private gs: GsService,
  ) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit(): void {
    this.getTableData();
  }
  ngAfterViewInit() {
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getTableData(): void {
    this.gs.getFuelTable().subscribe(f => {
      this.dataSource = new MatTableDataSource(f), 
      this.dataSource.paginator = this.paginator,
      this.dataSource.sort = this.sort
    });
  }
}

