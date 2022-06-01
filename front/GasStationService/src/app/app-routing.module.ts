import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile/profile.component';
import { DashComponent } from './dash/dash.component';
import { TableComponent } from './table/table.component';
import { GsdetailComponent } from './gsdetail/gsdetail.component';
import { LoginformComponent } from './loginform/loginform.component';
import { GsNewComponent } from './forms/gs-new/gs-new.component';
import { GsEditComponent } from './forms/gs-edit/gs-edit.component';
import { FuelTableComponent } from './fuel-table/fuel-table.component';
import { UsertableComponent } from './usertable/usertable.component';

const routes: Routes = [
  { path: 'loginform', component: LoginformComponent },
  { path: 'dash', component: DashComponent },
  { path: 'table', component: TableComponent },
  { path: 'gsdetail/:id', component: GsdetailComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'gsnew', component: GsNewComponent },
  { path: 'gsedit/:id', component: GsEditComponent },
  { path: 'fueltable', component: FuelTableComponent },
  { path: 'usertable', component: UsertableComponent },
  { path: '', redirectTo: 'dash', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
