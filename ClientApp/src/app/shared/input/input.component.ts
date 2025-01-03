import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-input',
  imports: [],
  template: `
    <div class="relative border rounded border-slate-300 m-2 p-2 ">
      <input class="w-full" [type]="type"  [value]="value" (input)="onChange($event)" [placeholder]="placeholder" [disabled]="disabled" [readOnly]="readOnly" >
      <label class="absolute bg-white text-xs -top-2 left-4 px-2">
        {{label}}
      </label>
    </div>`,
  styleUrl: './input.component.scss',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => InputComponent),
    multi: true
  }]
})

export class InputComponent implements ControlValueAccessor {
  @Input() placeholder = "";
  @Input() type = "text";
  @Input() readOnly = false;
  @Input({ required: true }) label: string = ''

  disabled = false
  onTouched = () => { }
  onChange = (_: any) => { }
  value = ''

  writeValue(value: any): void {
    this.value = value
  }
  registerOnChange(fn: any): void {
    this.onChange = (event: any) => fn(event.target.value)
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled
  }

}
