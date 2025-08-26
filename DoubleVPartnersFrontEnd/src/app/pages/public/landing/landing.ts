import { Component, inject, input } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { LandingService } from './landing.service';
import { passwordsMatchValidator } from '@app/shared/validators';
import { InputText } from '@components/input-text/input-text';

@Component({
  selector: 'app-landing',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    RouterModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    InputText
  ],
  standalone: true,
  templateUrl: './landing.html',
  styleUrl: './landing.css'
})
export class Landing {
  fb = inject(FormBuilder)
  private _router = inject(Router)
  private _landing = inject(LandingService)
  
  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      password1: ['', Validators.required],
      password2: ['', [Validators.required]],
    }, { validators: passwordsMatchValidator });
  }
  
  onSubmit(): void {
    this.form.markAllAsTouched();
    this.form.updateValueAndValidity();
    if(this.form.valid) {
      const {
        name,
        email,
        password1
      } = this.form.value
      this._landing.signup(name, email, password1)
      .subscribe(resp => {
        this._router.navigate(['/login'])
      })
    }
  }
}
