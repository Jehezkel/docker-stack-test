import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormConfig } from '../../master-data-page.component';
import { InputComponent } from '../../../shared/input/input.component';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-generic-form',
  imports: [ReactiveFormsModule, InputComponent, NgFor],
  templateUrl: './generic-form.component.html',
  styleUrl: './generic-form.component.scss'
})
export class GenericFormComponent implements OnInit {
  @Output() close = new EventEmitter<any>
  @Input() formConfig!: FormConfig
  @Input() inputValue: any | undefined

  formGroup = new FormGroup({})

  ngOnInit(): void {
    this.prepareFormGroup();
    if (this.inputValue) {
      this.formGroup.patchValue(this.inputValue)
    }
  }

  private prepareFormGroup() {
    this.formConfig!.fields.forEach((field) => {
      this.formGroup.addControl(field.field, new FormControl("", field.validators));
    });
  }

  onSubmit() {
    if (this.formGroup.valid) {
      this.close.emit(this.formGroup.value)
    }
  }
}
