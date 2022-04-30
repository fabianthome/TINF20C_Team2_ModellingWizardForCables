import {Component, OnDestroy, OnInit} from '@angular/core';
import {DataService} from "../../services/data.service";
import {BehaviorSubject, combineLatest, map, Observable, switchMap} from "rxjs";
import {ProductDetails} from "../../models/product-details";

@Component({
  selector: 'app-cable-search-grid',
  templateUrl: './cable-search-grid.component.html',
  styleUrls: ['./cable-search-grid.component.scss'],
})
export class CableSearchGridComponent implements OnInit, OnDestroy {
  products$: Observable<ProductDetails[]>
  filteredProducts$: Observable<ProductDetails[]>
  filterOptions$: Observable<FilterOptions>

  constructor(private dataService: DataService) {
    this.filterOptions$ = new BehaviorSubject<FilterOptions>({
      nameMustContain: "",
    })
    this.products$ = this.dataService.getProductList().pipe(
      switchMap(ids => combineLatest(ids.map(id => this.dataService.getProductDetails(id))))
    )
    this.filteredProducts$ = combineLatest([this.products$, this.filterOptions$]).pipe(
      map(([product, filter]) => this.filterProducts(product, filter))
    )
  }

  private filterProducts(products: ProductDetails[], filter: FilterOptions) {
    return products.filter(product => !product.name.includes(filter.nameMustContain))
  }


  ngOnInit(): void {
  }

  ngOnDestroy() {
  }

  routerLink(product: ProductDetails) {
    return `details/${product.id}`;
  }
}

interface FilterOptions {
  nameMustContain: string
}
