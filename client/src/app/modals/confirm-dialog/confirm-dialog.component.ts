import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent {
  title='Confirmation';
  message='Are you sure you want to do this ?';
  btnOkText='OK';
  btnCancelText='Cancel';
  result: boolean = false;

  constructor( public bsModalRef:BsModalRef) {

   }
   confirm(){
     this.result=true;
     this.bsModalRef.hide();
   }
    decline(){
      this.result=false;
      this.bsModalRef.hide();
    }

}
