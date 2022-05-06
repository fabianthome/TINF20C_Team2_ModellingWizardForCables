import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { delay, filter, map, Subscription, switchMap } from 'rxjs';
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
    private dataService: DataService,
    private router: Router
  ) {}

  private cableSubscription: Subscription | undefined;
  editorPath: boolean = false;
  caexVersion3: boolean = false;
  caexVersion2: boolean = false;

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
    console.log(this.router.url);
    if (this.router.url == '/editor') {
      this.editorPath = true;
    }
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
    this.dataService.createProduct(this.cable).subscribe((res) => {
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

  deleteCable() {
    this.dataService.deleteProductDetails(this.cable.id).subscribe((res) => {});
    delay(1000);
  }

  addPin(connector: any) {
    connector.pins.push({ name: 'P', connectedWire: '' });
    console.log(connector);
  }

  cancelEdit() {}
}
