import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';

export class LoginData {
    userName: string;
    password: string;
}

@Component({
  selector: 'd-login-dialog',
  templateUrl: 'login-dialog.component.html',
})
export class LoginDialogComponent {
    
  constructor(
      public dialogRef: MatDialogRef<LoginDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public loginData: LoginData
      ) {
    loginData = new LoginData();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}