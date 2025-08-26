import { AbstractControl, ValidationErrors } from "@angular/forms";

export const passwordsMatchValidator = (formGroup: AbstractControl): ValidationErrors | null => {
  const password1 = formGroup.get('password1')?.value;
  const password2 = formGroup.get('password2')?.value;
  return (password1 && password2 && password1 !== password2) ? { passwordsMismatch: true } : null;
}