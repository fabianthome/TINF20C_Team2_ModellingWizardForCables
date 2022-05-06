import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ProductDetails } from '../../models/product-details';

@Component({
  selector: 'app-cable-search-grid',
  templateUrl: './cable-search-grid.component.html',
  styleUrls: ['./cable-search-grid.component.scss'],
})
export class CableSearchGridComponent implements OnInit, OnDestroy {
  @Input()
  products: ProductDetails[] | null = null;

  constructor() {}

  ngOnInit(): void {}

  ngOnDestroy() {}

  routerLink(product: ProductDetails) {
    return `details/${product.id}`;
  }

  downloadFile() {
    let link = document.createElement('a');
    link.download = 'Cables_28032022.amlx';
    link.href =
      '/Users/kevinpauer/Documents/Developement/TINF20C_Team2_ModellingWizardForCables/SOURCE/CableWizard/Backend/Workdir/Cables_28032022.amlx';
    link.click();
  }
}
