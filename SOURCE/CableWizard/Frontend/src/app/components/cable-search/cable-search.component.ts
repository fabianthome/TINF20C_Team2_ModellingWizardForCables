import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatDrawer} from '@angular/material/sidenav';
import {DrawerService} from 'src/app/services/drawer.service';
import {FilterOptions} from '../../models/filter-options';
import {
  BehaviorSubject,
  combineLatest,
  map,
  Observable,
  switchMap,
} from 'rxjs';
import {DataService} from '../../services/data.service';
import {ProductDetails} from '../../models/product-details';
import {saveAs as importedSaveAs} from "file-saver";

@Component({
  selector: 'app-cable-search',
  templateUrl: './cable-search.component.html',
  styleUrls: ['./cable-search.component.scss'],
})
export class CableSearchComponent implements OnInit, AfterViewInit {
  @ViewChild('drawer')
  public drawer: MatDrawer | undefined;
  public filter: FilterOptions;
  public filteredProducts$: Observable<ProductDetails[]>;

  private allProducts: Observable<ProductDetails[]>;
  private filter$: BehaviorSubject<FilterOptions>;

  constructor(
    public dataService: DataService,
    public drawerService: DrawerService
  ) {
    this.filter = new FilterOptions();
    this.filter$ = new BehaviorSubject<FilterOptions>(this.filter);

    this.allProducts = this.dataService
      .getProductList()
      .pipe(
        switchMap((ids) =>
          combineLatest(ids.map((id) => this.dataService.getProductDetails(id)))
        )
      );
    this.filteredProducts$ = combineLatest([
      this.allProducts,
      this.filter$,
    ]).pipe(
      map(([products, filter]) =>
        CableSearchComponent.filterProducts(products, filter)
      )
    );
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.drawerService.setDrawer(this.drawer!);
  }

  onFilterChanged() {
    console.log('Updated search filter:');
    console.log(this.filter);
    this.filter$.next(this.filter);
  }

  private static filterProducts(
    products: ProductDetails[],
    filter: FilterOptions
  ) {
    return filter.apply(products);
  }

  downloadFile(caexVersion: string) {
    if (caexVersion == '2.15') {
      this.dataService.getFile('2_15').subscribe((data) => {
        console.log(data);
        const blob = new Blob([data], {type: 'application/force-download'});
        importedSaveAs(blob, 'CAEX_2_15.aml');
        // const blob = new Blob([data], { type: 'application/force-download' });
        // const url = window.URL.createObjectURL(blob);
        // window.open(url);
      });
    } else if (caexVersion == '3.0') {
      this.dataService.getFile('3_0').subscribe((data) => {
        console.log(data);
        const blob = new Blob([data], {type: 'application/force-download'});
        importedSaveAs(blob, 'CAEX_3_0.aml');
        // const url = window.URL.createObjectURL(blob);
        // window.open(url);
      });
    }
  }
}
