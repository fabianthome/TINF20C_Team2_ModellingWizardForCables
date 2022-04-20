import {Component, OnDestroy, OnInit} from '@angular/core';
import {DataService} from "../../services/data.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-cable-search-grid',
  templateUrl: './cable-search-grid.component.html',
  styleUrls: ['./cable-search-grid.component.scss'],
})
export class CableSearchGridComponent implements OnInit, OnDestroy {
  productIds$ : Observable<string[]>;

  constructor(private dataService: DataService) {
    this.productIds$ = this.dataService.getProductList().pipe()
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
  }

}
