import {Component, OnDestroy, OnInit} from '@angular/core';
import {DataService} from "../../services/data.service";
import {Observable} from "rxjs";
import {ProductInfo} from "../../models/product-info";

@Component({
  selector: 'app-cable-search-grid',
  templateUrl: './cable-search-grid.component.html',
  styleUrls: ['./cable-search-grid.component.scss'],
})
export class CableSearchGridComponent implements OnInit, OnDestroy {
  products$ : Observable<ProductInfo[]>;

  constructor(private dataService: DataService) {
    this.products$ = this.dataService.getProductList().pipe()
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
  }

}
