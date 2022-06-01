import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import { TokenStorageService } from '../_services/token-storage.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  currentUser: any;
  currentToken: any;
  currentRole: any;
  newpass: string = "";
  isChangePass = false;
  hide = false;
  
  constructor(
    private token: TokenStorageService,
    private auth: AuthenticationService,
    ) { }

  ngOnInit(): void {
    this.currentUser = this.token.getUser();
    this.currentToken = this.token.getToken();
    this.currentRole = this.token.getRole();
  }

  changePass(): void {
    this.isChangePass = true;
  }

  change(): void {
    this.auth.changepass(this.token.getId(), this.newpass);
    location.reload();
  }
}
