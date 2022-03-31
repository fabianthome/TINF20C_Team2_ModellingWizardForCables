import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {filter, map, Subscription, switchMap} from 'rxjs';
import {DataService} from "../../services/data.service";

@Component({
  selector: 'app-cable-details',
  templateUrl: './cable-details.component.html',
  styleUrls: ['./cable-details.component.scss'],
})
export class CableDetailsComponent implements OnInit, OnDestroy {

  cableText: string = "loading...";

  private routeSubscription: Subscription | undefined; // is this needed?
  private cableSubscription: Subscription | undefined;

  constructor(private route: ActivatedRoute, private api: DataService) {
  }

  ngOnInit(): void {
    this.routeSubscription = this.route.params.subscribe((params) => {
      console.log(params); //log the entire params object
      console.log(params['id']); //log the value of id
    });

    this.cableSubscription = this.route.params.pipe(
      map(params => params['id']),
      filter(id => typeof id == 'string'),
      switchMap(id => this.api.getCable(id as string)),
    ).subscribe(
      cableInfo => this.cableText = JSON.stringify(cableInfo)
    )
  }

  ngOnDestroy() {
    this.cableSubscription?.unsubscribe();
  }
}
