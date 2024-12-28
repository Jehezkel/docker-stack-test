import { AsyncPipe, NgClass, NgFor, NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ToastrService } from './toastr.service';

@Component({
  selector: 'app-toastr',
  imports: [NgClass, NgFor, AsyncPipe, NgIf],
  templateUrl: './toastr.component.html',
  styleUrl: './toastr.component.scss'
})
export class ToastrComponent {
  private toastrService = inject(ToastrService)
  notifications$ = this.toastrService.notifications$
  //constructor() {
  //  this.toastrService.info("Sample info", undefined, 2000)
  //  this.toastrService.error("Error!!!!", "xDDDDDDD", 4000)
  //  this.toastrService.success("IWIN", undefined, 5000)
  //  this.toastrService.warn("Warningg", undefined, 5000)
  //}
}

