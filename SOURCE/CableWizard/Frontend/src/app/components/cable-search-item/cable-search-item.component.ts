import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {DataService} from "../../services/data.service";
import {Subscription} from "rxjs";
import {ProductDetails} from "../../models/product-details";

@Component({
  selector: 'app-cable-search-item',
  templateUrl: './cable-search-item.component.html',
  styleUrls: ['./cable-search-item.component.scss']
})
export class CableSearchItemComponent implements OnInit, OnDestroy {

  @Input() productId: string | undefined

  productDetails: ProductDetails | undefined;

  private subscription: Subscription | undefined;

  constructor(private dataService: DataService) {
    if (this.productId != null) {
      this.subscription = this.dataService.getProductDetails(this.productId).subscribe(
        details => this.productDetails = details
      )
    }
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe()
  }

}
