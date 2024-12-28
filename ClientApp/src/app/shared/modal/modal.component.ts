import { AsyncPipe, NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ModalService } from './modal.service';

@Component({
  selector: 'app-modal',
  imports: [NgIf, AsyncPipe],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent {
  Cancel() {
    this.modalService.hide(false)
  }
  Confirm() {
    this.modalService.hide(true)
  }

  modalService = inject(ModalService)
  modalVisible = this.modalService.modalVisible$
}
