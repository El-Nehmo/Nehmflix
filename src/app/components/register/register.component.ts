import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule , FormGroup, FormControl, Validators} from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  registerForm = new FormGroup({
    username: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
  });

  onSubmit() {
    if (this.registerForm.valid) {
      const { username, email, password } = this.registerForm.value;
      console.log('Inscription avec :', username, email, password);
      //API .NET plus tard ici
    } else {
      console.warn('Formulaire d’inscription invalide');
    }
  }

}
