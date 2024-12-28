import { Injectable } from '@angular/core';
import { BehaviorSubject, delay, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastrService {
  private notificationsSubject = new BehaviorSubject<Notification[]>([])
  private counter = 0;
  notifications$ = this.notificationsSubject.asObservable()

  error(title: string, details?: string, duration: number = 3000) {
    this.addNotification('error', title, duration, details)
  }

  warn(title: string, details?: string, duration: number = 3000) {
    this.addNotification('warning', title, duration, details)
  }

  success(title: string, details?: string, duration: number = 3000) {
    this.addNotification('success', title, duration, details)
  }

  info(title: string, details?: string, duration: number = 3000) {
    this.addNotification('info', title, duration, details)
  }

  private addNotification(level: level, title: string, duration: number, details?: string) {
    const newNotification: Notification = { title: title, level: level, id: ++this.counter, duration: duration, details: details }

    this.notificationsSubject.next([...this.notificationsSubject.value, newNotification])
    setTimeout(() => this.removeNotification(newNotification.id), duration)
  }


  private removeNotification(id: number) {
    const filteredNotifications = this.notificationsSubject.value.filter(n => n.id !== id)
    this.notificationsSubject.next(filteredNotifications)
  }
}
export interface Notification {
  id: number
  title: string
  details?: string
  level: level
  duration: number
}
type level = 'error' | 'warning' | 'info' | 'success'

