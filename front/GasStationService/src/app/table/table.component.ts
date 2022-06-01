import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { GasStation} from '../_models/gs';
import { GsService } from '../_services/gs.service';
import {MatFormFieldModule} from '@angular/material/form-field';
@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})

export class TableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'address', 'phoneNumber'];
  dataSource: MatTableDataSource<GasStation>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private gsService: GsService) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit(): void {
    this.getGasStationData();
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

  getGasStationData(): void {
    this.gsService.getGasStationData().
      subscribe((gsdata: any) => {
        this.dataSource = new MatTableDataSource(gsdata),
        this.dataSource.paginator = this.paginator,
        this.dataSource.sort = this.sort
      });
  }
}
