import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {filter, map, Subscription, switchMap} from 'rxjs';
import {DataService} from 'src/app/services/data.service';
import {ProductDetails, DefaultCableDetails} from '../../models/product-details';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss'],
})
export class EditorComponent implements OnInit, OnDestroy {
  cable: ProductDetails = DefaultCableDetails

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) {
  }

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

  addConnector() {
    this.cable.connectors.push({
      type: 'M12A3PinMale',
      pins: [
        {
          name: '1',
          connectedWire: 'C1',
        },
        {
          name: '2',
          connectedWire: 'C2',
        },
        {
          name: '3',
          connectedWire: 'C3',
        },
        {
          name: '4',
          connectedWire: 'C4',
        },
      ],
    });
  }

  addWire() {
    const countOfWires = this.cable.wires.length;
    this.cable.wires.push('C' + (countOfWires + 1));
  }

  confirmEdit() {
    console.log(this.cable);
  }

  cancelEdit() {
    //todo
  }
}
