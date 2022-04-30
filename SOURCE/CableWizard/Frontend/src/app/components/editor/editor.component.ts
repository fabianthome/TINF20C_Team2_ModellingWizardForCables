import { Component, OnInit } from '@angular/core';
import {
  Attributes,
  Cable,
  cable,
  Connector,
  Pin,
} from '../../models/cable.models';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss'],
})
export class EditorComponent implements OnInit {
  attributes: Attributes = cable.attributes;
  cableId: number = cable.id;
  cableName: string = cable.name;
  wires: string[] = cable.wires;
  connectors: Connector[] = cable.connectors;
  connectorType: string = cable.connectors[0].type;
  pins: Pin[] = cable.connectors[0].pins;

  constructor() {}
  cable: Cable = {
    attributes: this.attributes,
    cableId: cable.id,
    cableName: cable.name,
    wires: cable.wires,
    connectors: cable.connectors,
    connectorType: cable.connectors[0].type,
    pins: cable.connectors[0].pins,
  };

  ngOnInit(): void {
    console.log(this.cable);
  }

  addConnector() {
    this.cable.connectors.push({
      type: 'M12A3PinMale',
      pins: [
        {
          type: 'PinType',
          name: '1',
          connectedWire: 'C1',
        },
        {
          type: 'PinType',
          name: '2',
          connectedWire: 'C2',
        },
        {
          type: 'PinType',
          name: '3',
          connectedWire: 'C3',
        },
        {
          type: 'PinType',
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
}
