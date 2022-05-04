import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Subscription, switchMap } from 'rxjs';
import { DataService } from 'src/app/services/data.service';
import { cable } from '../../models/cable.models';
import {
  ProductDetails,
  ProductAttributes,
  Connector,
  Pin,
  Wire,
} from '../../models/product-details';
@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss'],
})
export class EditorComponent implements OnInit {
  attributes: ProductAttributes = cable.attributes;
  id: string = cable.id;
  name: string = cable.name;
  wires: Wire[] = cable.wires;
  library: string = cable.library;
  connectors: Connector[] = cable.connectors;
  connectorType: string = cable.connectors[0].type;
  pins: Pin[] = cable.connectors[0].pins;
  attachedImagePaths: string[] = cable.attachedImagePaths;
  attachedFilePaths: string[] = cable.attachedFilePaths;
  cableText: any;

  cable: ProductDetails = {
    attachedImagePaths: this.attachedImagePaths,
    attachedFilePaths: this.attachedFilePaths,
    id: this.id,
    library: this.library,
    name: this.name,
    attributes: this.attributes,
    connectors: cable.connectors,
    wires: cable.wires,
  };
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
        this.cableText = JSON.stringify(cableInfo);
        // const cable: ProductDetails = {
        //   attachedImagePaths: cableInfo.attachedImagePaths,
        //   attachedFilePaths: cableInfo.attachedFilePaths,
        //   id: cableInfo.id,
        //   library: cableInfo.library,
        //   name: cableInfo.name,
        //   attributes: cableInfo.attributes,
        //   connectors: cableInfo.connectors,
        //   wires: cableInfo.wires,
        // };
        this.cable = cableInfo;
        console.log(this.cable);
      });
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
