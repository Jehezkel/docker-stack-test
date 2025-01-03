import { AsyncPipe, NgIf } from '@angular/common';
import { Component, inject, OnInit, ViewContainerRef, ViewChild, OnDestroy, ComponentRef } from '@angular/core';
import { ModalService } from './modal.service';
import { delay, filter, Subscription } from 'rxjs'

@Component({
  selector: 'app-modal',
  imports: [NgIf, AsyncPipe],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent implements OnInit, OnDestroy {
  @ViewChild('componentContainer', { read: ViewContainerRef, static: false }) modalContainer!: ViewContainerRef;

  componentRef: ComponentRef<any> | null = null
  modalService = inject(ModalService)
  modalVisible = this.modalService.modalVisible$
  subsToDestroy: Subscription[] = []

  ngOnInit(): void {

    const visabilitySub = this.modalService.modalVisible$
      .pipe(delay(0), filter(isVisible => isVisible && this.modalService.componentToProject !== null))
      .subscribe(_ => this.renderComponent())
    this.subsToDestroy.push(visabilitySub)

  }

  ngOnDestroy(): void {
    this.subsToDestroy.forEach(sub => sub.unsubscribe())
  }

  Cancel() {
    this.modalService.hide(false)
  }

  Confirm() {
    this.modalService.hide(true)
  }

  renderComponent() {

    if (!this.modalContainer || !this.modalService.componentToProject) {
      throw Error("Missing container")
    }

    this.modalContainer.clear()
    this.componentRef = this.modalContainer.createComponent(this.modalService.componentToProject)
    const inputs = this.modalService.componentsInput;
    if (inputs) {
      Object.keys(inputs).forEach(inputKey => {
        this.componentRef!.setInput(inputKey, inputs[inputKey])
      })
    }
    if (this.componentRef.instance.close) {
      this.componentRef.instance.close.subscribe((result: any) => this.modalService.hide(result))
    }
  }

}
