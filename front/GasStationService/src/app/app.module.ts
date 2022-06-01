import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProfileComponent } from './profile/profile.component';
import { TableComponent } from './table/table.component';
import { DashComponent } from './dash/dash.component';
import { FooterComponent } from './footer/footer.component';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HTTPInterceptorService } from './_helpers/authentication.interceptor';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';

import { LoginformComponent } from './loginform/loginform.component';
import { NavbarComponent } from './navbar/navbar.component';
import { GsdetailComponent } from './gsdetail/gsdetail.component';
import { GsNewComponent } from './forms/gs-new/gs-new.component';
import { GsEditComponent } from './forms/gs-edit/gs-edit.component';
import { MatSelectModule} from '@angular/material/select';
import {MatSortModule} from '@angular/material/sort';
import { FuelTableComponent } from './fuel-table/fuel-table.component';
import { newUserDialog, UsertableComponent } from './usertable/usertable.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [
    AppComponent,
    ProfileComponent,
    TableComponent,
    DashComponent,
    LoginformComponent,
    FooterComponent,
    NavbarComponent,
    GsdetailComponent,
    GsNewComponent,
    GsEditComponent,
    FuelTableComponent,
    UsertableComponent,
    newUserDialog
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    BrowserAnimationsModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatPaginatorModule,
    MatGridListModule,
    MatCardModule,
    MatInputModule,
    MatSelectModule,
    MatSortModule,
    MatDialogModule,
    MatIconModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HTTPInterceptorService,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
