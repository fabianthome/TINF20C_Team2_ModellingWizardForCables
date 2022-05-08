import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  delay,
  filter,
  map,
  Observable,
  startWith,
  Subscription,
  switchMap,
} from 'rxjs';
import {
  PossibleConnectors,
  PossibleModels,
} from 'src/app/models/possible-connectos.model';
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
  myControl = new FormControl();
  possibleConnectors: PossibleModels[] = [];
  numbers: number[] = [2, 3];
  filteredOptions!: Observable<PossibleModels[]>;

  typeMale: any = '';
  typeFemale: any = '';
  subWorked: boolean = false;

  standardTypeMale: string = '';
  standardRouteMale: string = '';
  standardTypeFemale: string = '';
  standardRouteFemale: string = '';

  ngOnInit(): void {
    this.cableSubscription = this.route.params
      .pipe(
        map((params) => params['id']),
        filter((id) => typeof id == 'string'),
        switchMap((id) => this.dataService.getProductDetails(id as string))
      )
      .subscribe((cableInfo) => {
        this.subWorked = true;
        this.cable = cableInfo;
        this.standardTypeMale = this.cable.connectors[0].type;
        console.log(this.standardTypeMale);
        this.standardRouteMale = this.cable.connectors[0].path;
        this.standardTypeFemale = this.cable.connectors[1].type;
        console.log(this.standardTypeFemale);
        this.standardRouteFemale = this.cable.connectors[1].path;
        console.log(this.cable);

        this.dataService.getPossibleConnectors().subscribe((res) => {
          this.possibleConnectors = res;
          this.possibleConnectors.forEach((element) => {
            if (element.item1 == this.standardTypeMale) {
              this.standardRouteMale = element.item2;
              console.log(this.standardRouteMale);
            }
          });
          this.possibleConnectors.forEach((element) => {
            if (element.item1 == this.standardTypeFemale) {
              this.standardRouteFemale = element.item2;
              console.log(this.standardRouteFemale);
            }
          });
        });
      });

    if (!this.subWorked) {
      this.subWorked = false;
      this.dataService.getPossibleConnectors().subscribe((res) => {
        this.possibleConnectors = res;
        this.possibleConnectors.forEach((element) => {
          if (element.item1 == this.standardTypeMale) {
            this.standardRouteMale = element.item2;
            console.log(this.standardRouteMale);
          }
        });
        this.possibleConnectors.forEach((element) => {
          if (element.item1 == this.standardTypeFemale) {
            this.standardRouteFemale = element.item2;
            console.log(this.standardRouteFemale);
          }
        });
      });
    }

    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map((value) => (typeof value === 'string' ? value : value.name)),
      map((name) =>
        name ? this._filter(name) : this.possibleConnectors.slice()
      )
    );

    if (this.router.url == '/editor') {
      this.editorPath = true;
    }
  }

  ngOnDestroy() {
    this.cableSubscription?.unsubscribe();
  }

  displayFn(user: PossibleModels): string {
    return user && user.item1 ? user.item1 : '';
  }

  private _filter(name: string): PossibleModels[] {
    const filterValue = name.toLowerCase();

    return this.possibleConnectors.filter((possibleConnectors) =>
      possibleConnectors.item1.toLowerCase().includes(filterValue)
    );
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
    console.log(typeof this.cable.connectors[0].type);
    if (this.typeMale.item1 == undefined) {
      this.cable.connectors[0].type = this.standardTypeMale;
      this.cable.connectors[0].path = this.standardRouteMale;
      this.cable.connectors[1].type = this.standardTypeFemale;
      this.cable.connectors[1].path = this.standardRouteFemale;
    } else if (this.typeFemale.item1 == undefined) {
      this.cable.connectors[0].type = this.standardTypeMale;
      this.cable.connectors[0].path = this.standardRouteMale;
      this.cable.connectors[1].type = this.standardTypeFemale;
      this.cable.connectors[1].path = this.standardRouteFemale;
    } else {
      this.cable.connectors[0].type = this.typeMale.item1;
      this.cable.connectors[1].type = this.typeFemale.item1;
      this.cable.connectors[0].path = this.typeMale.item2;
      this.cable.connectors[1].path = this.typeFemale.item2;
    }

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
