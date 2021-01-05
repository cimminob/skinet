import {
  Component,
  ElementRef,
  Input,
  OnInit,
  Self,
  ViewChild,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss'],
})

/*
Defines an interface that acts as a bridge between the Angular forms API 
and a native element in the DOM. The native element we need to access is
the input field
*/
export class TextInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('input', { static: true }) input: ElementRef;
  @Input() type = 'text';
  @Input() label: string;

  //inject the angular formControl to access its properties directly. NgControl
  //is a base class formControls derive from. The @Self decorator tells angular
  //not to look elsewhere for the NgControl dependency, rather the resolution
  //starts in this class
  constructor(@Self() public controlDir: NgControl) {
    //bind the controlDir to this class
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    //attempt to access the validators that have been set on this control
    //if control.validator exists, access the validator property or set to empty array
    const validators = control.validator ? [control.validator] : [];

    //async validators - used for APIs
    const asyncValidators = control.asyncValidator
      ? [control.asyncValidator]
      : [];

    //set the validators that were passed in. For example the control from the login form
    //will pass in its validators and they will be set here
    control.setValidators(validators);
    control.setAsyncValidators(asyncValidators);
    //validate the the form we just created
    control.updateValueAndValidity();
  }

  onChange(event) {}

  onTouched() {}

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  //fn a function passed from the ControlValueAccessor
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
