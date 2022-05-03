import { Component, OnInit } from '@angular/core';
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
  connectorType: string = cable.connectors[0].name;
  pins: Pin[] = cable.connectors[0].pins;
  attachedImagePaths: string[] = cable.attachedImagePaths;
  attachedFilePaths: string[] = cable.attachedFilePaths;

  constructor() {}
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

  ngOnInit(): void {
    console.log(this.cable);
  }

  addConnector() {
    this.cable.connectors.push({
      name: 'M12A3PinMale',
      pins: [
        {
          type: 'PinType',
          name: '1',
          connectedWireName: 'C1',
        },
        {
          type: 'PinType',
          name: '2',
          connectedWireName: 'C2',
        },
        {
          type: 'PinType',
          name: '3',
          connectedWireName: 'C3',
        },
        {
          type: 'PinType',
          name: '4',
          connectedWireName: 'C4',
        },
      ],
    });
  }

  addWire() {
    const countOfWires = this.cable.wires.length;
    this.cable.wires.push('C' + (countOfWires + 1) );
  }

  confirmEdit() {
    console.log(this.cable);
  }
}
