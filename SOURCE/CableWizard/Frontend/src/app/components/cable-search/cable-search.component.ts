import { Component, OnInit } from '@angular/core';
import {fromEvent} from "rxjs";

@Component({
  selector: 'app-cable-search',
  templateUrl: './cable-search.component.html',
  styleUrls: ['./cable-search.component.scss']
})
export class CableSearchComponent implements OnInit {
  searchQuery: string = "";

  resize$ = fromEvent(window, 'resize');
  resizeSubscription = this.resize$.subscribe()

  constructor() { }

  ngOnInit(): void {
  }

}
