import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Subscription, switchMap } from 'rxjs';
import { DataService } from 'src/app/services/data.service';
import {
  ProductDetails,
  DefaultCableDetails,
} from '../../models/product-details';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss'],
})
export class EditorComponent implements OnInit, OnDestroy {
  cable: ProductDetails = DefaultCableDetails;

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) {}

  private cableSubscription: Subscription | undefined;

  ngOnInit(): void {
    this.cableSubscription = this.route.params
      .pipe(
        map((params) => params['id']),
        filter((id) => typeof id == 'string'),
        switchMap((id) => this.dataService.getProductDetails(id as string))
      )
      .subscribe((cableInfo) => {
        this.cable = cableInfo;
      });
  }

  ngOnDestroy() {
    this.cableSubscription?.unsubscribe();
  }

  addWire() {
    const countOfWires = this.cable.wires.length;
    const number = this.cable.wires[countOfWires - 1].charAt(
      this.cable.wires[countOfWires - 1].length - 1
    );
    console.log(number);
    this.cable.wires.push('C' + (+number + 1));
  }

  confirmEdit() {
    console.log(this.cable);
    var encoded = btoa(JSON.stringify(this.cable));
    this.dataService
      .createProduct(this.cable.name, encoded)
      .subscribe((res) => {
        console.log(res);
      });
  }

  deleteWire(wireName: string) {
    const index = this.cable.wires.indexOf(wireName);
    if (index > -1) {
      this.cable.wires.splice(index, 1);
    }
  }

  deletePin(connector: any, pin: any) {
    console.log(connector, pin);
    const index = connector.pins.indexOf(pin);
    console.log(connector.pins);
    if (index > -1) {
      connector.pins.splice(index, 1);
    }
  }

  addPin(connector: any) {
    connector.pins.push({ name: 'P', connectedWire: '' });
    console.log(connector);
  }

  cancelEdit() {}
}
