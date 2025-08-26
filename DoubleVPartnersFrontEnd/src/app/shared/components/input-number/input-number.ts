import { NgStyle } from '@angular/common';
import { Component, forwardRef, input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { IconField } from 'primeng/iconfield';
import { InputIcon } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-input-number',
  standalone: true,
  imports: [
    NgStyle,
    InputTextModule,
    IconField,
    InputIcon
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputNumber),
      multi: true
    }
  ],
  templateUrl: './input-number.html',
  styleUrl: './input-number.css'
})
export class InputNumber implements ControlValueAccessor {
  id = input<string>('')
  label = input<string>('')
  placeholder = input<string>('')
  error = input<string | null>('')
  icon = input<string | null>(null)
  required = input(false)

  value: string = '';
  disabled: boolean = false;

  onChange = (_: any) => {};
  onTouched = () => {};

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  handleInput(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.value = target.value;
    this.onChange(this.value);
  }

  handleBlur(): void {
    this.onTouched();
  }

  get nameHelp(): string {
    return `${this.id}-help`;
  }
}
