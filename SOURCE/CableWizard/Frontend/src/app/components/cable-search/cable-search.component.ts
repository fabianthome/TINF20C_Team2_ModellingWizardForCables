import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatDrawer} from '@angular/material/sidenav';
import {DrawerService} from 'src/app/services/drawer.service';
import {FilterOptions} from "../../models/filter-options";
import {BehaviorSubject, combineLatest, map, Observable, switchMap} from "rxjs";
import {DataService} from "../../services/data.service";
import {ProductDetails} from "../../models/product-details";

@Component({
  selector: 'app-cable-search',
  templateUrl: './cable-search.component.html',
  styleUrls: ['./cable-search.component.scss'],
})
export class CableSearchComponent implements OnInit, AfterViewInit {
  @ViewChild('drawer')
  public drawer: MatDrawer | undefined;
  public filter: FilterOptions;
  public filter$: BehaviorSubject<FilterOptions>
  public products$: Observable<ProductDetails[]>
  public filteredProducts$: Observable<ProductDetails[]>;

  constructor(public dataService: DataService, public drawerService: DrawerService) {
    this.filter = {
      queryText: "",
      widthMax: null,
      widthMin: null,
      heightMax: null,
      heightMin: null,
      lengthMax: null,
      lengthMin: null,
      weightMax: null,
      weightMin: null,
      temperatureMax: null,
      temperatureMin: null,
    }
    this.filter$ = new BehaviorSubject<FilterOptions>(this.filter)

    this.products$ = this.dataService.getProductList().pipe(
      switchMap(ids => combineLatest(ids.map(id => this.dataService.getProductDetails(id))))
    )
    this.filteredProducts$ = combineLatest([this.products$, this.filter$]).pipe(
      map(([products, filter]) => filterProducts(products, filter))
    )

  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.drawerService.setDrawer(this.drawer!)
  }

  filterChanged() {
    this.filter$.next(this.filter)
  }
}

function filterProducts(products: ProductDetails[], filter: FilterOptions) {
  return products.filter(product => {

    if (!product.name.includes(filter.queryText)) return false;

    return true;

  });
}
