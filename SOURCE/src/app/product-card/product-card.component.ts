import { Component, OnInit } from '@angular/core';
import { Product } from "../product";

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  product: Product = {
    name: "USB-Connector",
    description: "This is a placeholder"
  }

  constructor() {
  }

  ngOnInit(): void {
  }

}
