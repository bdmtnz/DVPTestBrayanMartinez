import { Component, inject, input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InputText } from '@components/input-text/input-text';
import { AuthService } from '@app/shared/services/auth.service';
import { ButtonModule } from 'primeng/button';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-login',
  imports: [
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    InputText
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {
  fb = inject(FormBuilder)
  private router = inject(Router)
  private _auth = inject(AuthService)

  redirectFrom = input<string>()
  redirectTo = input<string>()

  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    this.form.markAllAsTouched();
    this.form.updateValueAndValidity();
    if(this.form.valid) {
      this._auth.login(this.form.value.username, this.form.value.password)
      .subscribe(resp => {
        this.router.navigate(['auth']);
      })
    }
  }
}
