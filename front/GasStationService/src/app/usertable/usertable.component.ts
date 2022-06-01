import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface Users {
  id: number,
  login: string,
  role: string
}
export interface DialogData {
  login: string,
  role: string,
  pass: string
}

@Component({
  selector: 'app-usertable',
  templateUrl: './usertable.component.html',
  styleUrls: ['./usertable.component.scss']
})
export class UsertableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'login', 'role', 'actions'];
  dataSource: MatTableDataSource<Users>;
  private newUser: DialogData = {login: '', role: 'User', pass:''};

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private auth: AuthenticationService,
    public dialog: MatDialog,
  ) { 
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit(): void {
    this.getUsers();
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

  getUsers(): void {
    this.auth.getUsers().subscribe( u => {
      this.dataSource = new MatTableDataSource(u), 
      this.dataSource.paginator = this.paginator,
      this.dataSource.sort = this.sort
    })
  }

  deleteUser(id: number): void {
    this.auth.deleteUser(id);
    location.reload();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(newUserDialog, {
      width: '250px',
      data: {login: this.newUser.login, role: this.newUser.role},
    });
  }
}

@Component({
  selector: 'new-user-dialog',
  templateUrl: 'newUserDialog.html',
})
export class newUserDialog {
  hide = true;
  constructor(
    private auth: AuthenticationService,
    public dialogRef: MatDialogRef<newUserDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  onYesClick(): void {
    this.auth.newUser(this.data);
    location.reload();
  }
}
