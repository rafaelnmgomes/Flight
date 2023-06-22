import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services/passenger.service'
import { FormBuilder, Validators } from '@angular/forms'
import { AuthService } from '../auth/auth.service'
import { Router, ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent implements OnInit {

  constructor(private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  requestedUrl?: string = undefined

  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
    firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(50)])],
    lastName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(50)])],
    isFemale: [true, Validators.required]
  })

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(p => this.requestedUrl = p['requestedUrl'])
  }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value ?? '' }

    this.passengerService.findPassenger(params).subscribe(this.login, e => {
      if (e.status != 404)
        console.error(e)
    })
  }

  register() {
    this.passengerService.registerPassenger({ body: this.form.value }).subscribe(this.login, console.error);
  }

  private login = () => {
    if (this.form.invalid) {
      return;
    }

    this.authService.loginUser({ email: this.form.get('email')?.value ?? '' })
    this.router.navigate([this.requestedUrl ?? '/search-flights'])
  }
}
