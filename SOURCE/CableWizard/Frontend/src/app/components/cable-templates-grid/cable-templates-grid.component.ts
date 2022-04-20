import {Component, OnDestroy, OnInit} from '@angular/core';
import {DataService} from "../../services/data.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-cable-templates-grid',
  templateUrl: './cable-templates-grid.component.html',
  styleUrls: ['./cable-templates-grid.component.scss'],
})
export class CableTemplatesGridComponent implements OnInit, OnDestroy {
  productIds$ : Observable<string[]>;

  constructor(private dataService: DataService) {
    this.productIds$ = this.dataService.getProductList().pipe()
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
  }

}
