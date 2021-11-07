import { Component, OnInit } from '@angular/core';
import { CONNECTORS } from "../connectors";

@Component({
  selector: 'app-product-table',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  title = 'Connectors'
  products = CONNECTORS

  constructor() {
  }

  ngOnInit(): void {
  }

}
