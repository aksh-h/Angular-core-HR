import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  constructor(public snackBar: MatSnackBar,
    private zone: NgZone) { }

    public open(message, action = 'X', duration = 4000) {
      debugger;
      this.zone.run(() => {
          this.snackBar.open(message, action, { duration, verticalPosition: 'top', horizontalPosition: 'end' });
      });
  }
}
