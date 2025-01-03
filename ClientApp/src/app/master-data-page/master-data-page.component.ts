import { Component } from '@angular/core';
import { GenericCRUDComponent, MasterData } from './generic-crud/generic-crud.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-master-data-page',
  imports: [GenericCRUDComponent],
  templateUrl: './master-data-page.component.html',
  styleUrl: './master-data-page.component.scss'
})
export class MasterDataPageComponent {
  currentConfig: EntityConfig
  constructor(activatedRoute: ActivatedRoute) {
    var currenPathName = activatedRoute.snapshot.routeConfig?.path
    const indexOfConfig = MasterData.findIndex(c => c.path === currenPathName)
    this.currentConfig = MasterData[indexOfConfig]
  }


}


export interface EntityConfig {
  entityName: string
  tableConfig: TableConfig
  formConfig: FormConfig
  path: string
  label: string

}

export interface TableConfig {
  columns: ColumnConfig[]
}

export interface ColumnConfig {
  header: string
  field: string
}

export interface FormConfig { fields: FieldConfig[] }

export interface FieldConfig {
  label: string
  field: string
  isEditable: boolean
  type: string
  validators: any[]
}
