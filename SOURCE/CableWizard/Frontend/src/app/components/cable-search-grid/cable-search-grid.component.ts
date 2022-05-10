import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { ProductDetails } from '../../models/product-details';
import { saveAs as importedSaveAs } from 'file-saver';

@Component({
  selector: 'app-cable-search-grid',
  templateUrl: './cable-search-grid.component.html',
  styleUrls: ['./cable-search-grid.component.scss'],
})
export class CableSearchGridComponent implements OnInit, OnDestroy {
  @Input()
  products: ProductDetails[] | null = null;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {}

  ngOnDestroy() {}

  routerLink(product: ProductDetails) {
    return `details/${product.id}`;
  }

  downloadFile(caexVersion: string) {
    if (caexVersion == '2.15') {
      this.dataService.getFile('2_15').subscribe((data) => {
        console.log(data);
        const blob = new Blob([data], { type: 'application/force-download' });
        importedSaveAs(blob, 'CAEX_2_15.aml');
        // const blob = new Blob([data], { type: 'application/force-download' });
        // const url = window.URL.createObjectURL(blob);
        // window.open(url);
      });
    } else if (caexVersion == '3.0') {
      this.dataService.getFile('3_0').subscribe((data) => {
        console.log(data);
        const blob = new Blob([data], { type: 'application/force-download' });
        importedSaveAs(blob, 'CAEX_3_0.aml');
        // const url = window.URL.createObjectURL(blob);
        // window.open(url);
      });
    }
  }
}
