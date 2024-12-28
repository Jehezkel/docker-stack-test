import { Injectable } from '@angular/core';
import { BehaviorSubject, filter, Observable, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  modalResultSubject = new BehaviorSubject<boolean | undefined>(undefined)
  modalResult$ = this.modalResultSubject.asObservable().pipe(filter(result => result !== undefined), take(1))
  private modalVisibleSubject$ = new BehaviorSubject<boolean>(false)
  modalVisible$: Observable<boolean> = this.modalVisibleSubject$.asObservable()
  confirmLabel = "Yes"
  cancelLabel = "Cancel"
  question = "Are you sure?";
  constructor() { }
  show(question: string, confirmLabel: string, cancelLabel: string) {
    this.cancelLabel = cancelLabel
    this.confirmLabel = confirmLabel
    this.question = question
    this.modalVisibleSubject$.next(true)
    this.modalResultSubject.next(undefined)
    return this.modalResult$
  }
  hide(result: boolean) {
    this.modalVisibleSubject$.next(false)
    this.modalResultSubject.next(result)
  }
}
