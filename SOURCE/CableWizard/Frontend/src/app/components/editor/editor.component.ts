import { Component, OnInit } from '@angular/core';
import { CableDetailsComponent } from '../cable-details/cable-details.component';

const cable = {
  id: 1,
  name: 'BCC M313-M313-30-300-EX43T2-050',
  attributes: {
    Length: 50,
    Manufacturer: 'Balluff',
  },
  wires: ['C1', 'C2', 'C3'],
  connectors: [
    {
      type: 'M12A3PinFemale',
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
    },
  ],
};

export interface Pin {
  type: string;
  name: string;
  connectedWire: string;
}

export interface Connector {
  type: string;
  pins: Pin[];
}

export interface Cable {
  cableId: number;
  cableName: string;
  wires: string[];
  connectors: Connector[];
  connectorType: string;
  pins: Pin[];
}

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss'],
})
export class EditorComponent implements OnInit {
  cableId: number = cable.id;
  cableName: string = cable.name;
  wires: string[] = cable.wires;
  connectors: Connector[] = cable.connectors;
  connectorType: string = cable.connectors[0].type;
  pins: Pin[] = cable.connectors[0].pins;

  constructor() {}
  cable: Cable = {
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
