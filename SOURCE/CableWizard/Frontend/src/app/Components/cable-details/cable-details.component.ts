import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-cable-details',
  templateUrl: './cable-details.component.html',
  styleUrls: ['./cable-details.component.scss'],
})
export class CableDetailsComponent implements OnInit {
  private routeSub: Subscription | undefined;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe((params) => {
      console.log(params); //log the entire params object
      console.log(params['id']); //log the value of id
    });
  }
}
