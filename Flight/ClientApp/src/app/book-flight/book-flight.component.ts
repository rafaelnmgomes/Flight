import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from '../api/services/flight.service'
import { FlightRm } from '../api/models'

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {

  constructor(private route: ActivatedRoute, private flightService: FlightService, private router: Router) { }

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  ngOnInit(): void {
    this.route.paramMap
      .subscribe(p => this.findFlight(p.get("flightId")));
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId })
      .subscribe(flight => this.flight = flight, this.handleError)
  }

  private handleError = (err: any) => {
    if (err.status != 200) {
      alert("An error occured")
      this.router.navigate(['/search-flights'])
    }
    console.log("Response error. Status: " + err.status)
    console.log("Response error. Status Text: " + err.statusText)
    console.log(err)
  }
}
