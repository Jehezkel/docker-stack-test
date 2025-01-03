import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  componentsInput: { [key: string]: any; } = {}

  modalResultSubject = new Subject<any>()
  modalResult$ = this.modalResultSubject.asObservable().pipe(take(1))

  private modalVisibleSubject$ = new BehaviorSubject<boolean>(false)
  modalVisible$: Observable<boolean> = this.modalVisibleSubject$.asObservable()
  title: string | undefined
  confirmLabel = "Yes"
  cancelLabel = "Cancel"
  question = "Are you sure?";
  componentToProject: any = null

  constructor() { }

  show(question: string, confirmLabel: string, cancelLabel: string, title: string) {
    this.cancelLabel = cancelLabel
    this.confirmLabel = confirmLabel
    this.question = question
    this.modalVisibleSubject$.next(true)
    this.modalResultSubject.next(undefined)
    this.title = title
    return this.modalResult$
  }

  showComponent(component: any, inputs: { [key: string]: any }, title?: string) {
    this.componentsInput = inputs
    this.componentToProject = component
    this.modalVisibleSubject$.next(true)
    this.title = title
    return this.modalResult$.pipe(take(1))
  }

  hide(result?: any) {
    if (result !== undefined) {
      this.modalResultSubject.next(result)
    }
    this.modalVisibleSubject$.next(false)
    this.componentToProject = null;
    this.componentsInput = {};
  }
}
