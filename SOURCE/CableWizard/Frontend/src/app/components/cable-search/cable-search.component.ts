import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cable-search',
  templateUrl: './cable-search.component.html',
  styleUrls: ['./cable-search.component.scss']
})
export class CableSearchComponent implements OnInit {
  searchQuery: string = "";

  constructor() { }

  ngOnInit(): void {
  }

}
