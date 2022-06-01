import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import * as shajs from 'sha.js';

@Component({
  selector: 'app-loginform',
  templateUrl: './loginform.component.html',
  styleUrls: ['./loginform.component.scss']
})
export class LoginformComponent implements OnInit {
  public msg!: string;
  constructor(private _auth: AuthenticationService) { }

  ngOnInit(): void {
    this.ResetMsg();
  }

  public ResetMsg(): void {
    this.msg = "Log in to continue";
  }
  public Login(info: { login: string, password: string }) {
    info.password = shajs('sha256').update(info.password).digest('hex');
    
    this._auth.login(JSON.parse(JSON.stringify(info))).subscribe(
      status => {
        if (status == 200) {
          this.msg = "Success";
          location.replace('/profile'); 
        }
        else if (status == 401)
          this.msg = "Wrong login/password";
        else
          this.msg = `Something went wrong (${status})`;
      });
  }
}
