import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {ProductInfo} from "../../models/product-info";
import {DataService} from "../../services/data.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-cable-search-item',
  templateUrl: './cable-search-item.component.html',
  styleUrls: ['./cable-search-item.component.scss']
})
export class CableSearchItemComponent implements OnInit, OnDestroy {

  @Input() productId: string | undefined

  productInfo: ProductInfo | undefined;

  private subscription: Subscription | undefined;

  constructor(private dataService: DataService) {
    if (this.productId != null) {
      this.subscription = this.dataService.getProductInfo(this.productId).subscribe(
        productInfo => this.productInfo = productInfo
      )
    }
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe()
  }

}
