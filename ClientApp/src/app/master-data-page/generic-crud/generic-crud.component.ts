import { Component, inject, Input, OnInit } from '@angular/core';
import { EntityConfig } from '../master-data-page.component';
import { AsyncPipe, NgFor } from '@angular/common';
import { Observable, Subject, filter, startWith, switchMap } from 'rxjs';
import { ApiClientService } from '../../shared/api-client.service';
import { ToastrService } from '../../shared/toastr/toastr.service';
import { ModalService } from '../../shared/modal/modal.service';
import { Validators } from '@angular/forms';
import { GenericFormComponent } from './generic-form/generic-form.component';


@Component({
  selector: 'app-generic-crud',
  imports: [NgFor, AsyncPipe],
  templateUrl: './generic-crud.component.html',
  styleUrl: './generic-crud.component.scss'
})
export class GenericCRUDComponent implements OnInit {
  @Input() config!: EntityConfig;

  modalService = inject(ModalService)
  toastrService = inject(ToastrService)
  apiClient = inject(ApiClientService)

  dataRows$ = new Observable<any[]>()
  refreshCall$ = new Subject<void>()

  onAddClick() {
    this.modalService.showComponent(GenericFormComponent, { formConfig: this.config.formConfig }, "Add Form")
      .pipe(switchMap(result => this.apiClient.createMasterDataRow(this.config.path, result)))
      .subscribe(
        {
          next:
            _ => {
              this.toastrService.success("Record added");
              this.refreshCall$.next();
            },
          error:
            _ => {
              this.toastrService.error("Error occured on adding a record");
              this.refreshCall$.next();
            }
        }
      )
  }

  onEditClick(record: any) {
    this.modalService.showComponent(GenericFormComponent, { formConfig: this.config.formConfig, inputValue: record }, "Edit Form")
      .pipe(switchMap(result => this.apiClient.editMasterDataRow(this.config.path, record.id, result)))
      .subscribe(
        {
          next:
            _ => {
              this.toastrService.success("Record updated");
              this.refreshCall$.next();
            },
          error:
            _ => {
              this.toastrService.error("Error occured on record update");
              this.refreshCall$.next();
            }
        }
      )
  }

  onDelete(row: any) {
    this.modalService.show(`Are you sure to delete this row ?`, "Delete", "Cancel", "Delete confirmation")
      .pipe(filter(result => result),
        switchMap(_ => this.apiClient.deleteMasterDataRow(this.config.path, row.id)))
      .subscribe(
        {
          next:
            _ => {
              this.toastrService.success("Record deleted");
              this.refreshCall$.next();
            },
          error:
            _ => {
              this.toastrService.error("Error occured on record deletion");
              this.refreshCall$.next();
            }
        }
      )
  }
  ngOnInit(): void {
    this.dataRows$ = this.refreshCall$.pipe(startWith(null), switchMap(_ => this.apiClient.getMasterDataRows(this.config.path)))

  }
}

export const CategoriesConfig: EntityConfig = {
  entityName: "category",
  path: "categories",
  label: "Categories",
  formConfig: {
    fields: [
      { type: "text", label: "Category Name", field: "name", "isEditable": true, validators: [Validators.required] }
    ]
  },
  tableConfig: {
    columns: [
      { header: "Category Name", field: "name" }
    ]
  }
}
export const Manufacturers: EntityConfig = {
  entityName: "manufacturer",
  path: "manufacturers",
  label: "Manufacturers",
  formConfig: {
    fields: [
      { type: "text", label: "Manufacturer Name", field: "name", "isEditable": true, validators: [Validators.required] }
    ]
  },
  tableConfig: {
    columns: [
      { header: "Manufacturer Name", field: "name" }
    ]
  }
}
export const MasterData: EntityConfig[] = [CategoriesConfig, Manufacturers]
