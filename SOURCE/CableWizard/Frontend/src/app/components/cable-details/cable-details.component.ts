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

  private cableSubscription: Subscription | undefined;

  constructor(private route: ActivatedRoute, private api: DataService) {
  }

  ngOnInit(): void {
    this.cableSubscription = this.route.params.pipe(
      map(params => params['id']),
      filter(id => typeof id == 'string'),
      switchMap(id => this.api.getProductDetails(id as string)),
    ).subscribe(
      cableInfo => this.cableText = JSON.stringify(cableInfo)
    )
  }

  ngOnDestroy() {
    this.cableSubscription?.unsubscribe();
  }
}
