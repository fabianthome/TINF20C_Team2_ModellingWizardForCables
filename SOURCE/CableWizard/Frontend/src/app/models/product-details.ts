export interface ProductDetails {
  attachedImagePaths: string[];
  attachedFilePaths: string[];
  id: string;
  name: string;
  library: string;
  attributes: ProductAttributes;
  connectors: Connector[];
  wires: Wire[];
}

export interface ProductAttributes {
  manufacturer: string | null;
  manufacturerUri: string | null;
  deviceClass: string | null;
  model: string | null;
  productCode: string | null;
  ipCode: string | null;
  material: string | null;
  length: number | null;
  width: number | null;
  height: number | null;
  weight: number | null;
  temperatureMin: number | null;
  temperatureMax: number | null;
}

export interface Connector {
  type: string;
  pins: Pin[];
}

export interface Pin {
  name: string;
  connectedWire: string;
}

// backend returns these as string not object!
export type Wire = string;

/*
export interface Wire {
  name: string;
}
*/

export const DefaultCableDetails: ProductDetails = {
  id: '',
  name: 'Cable Name',
  attachedImagePaths: ['./32edq', './32edq'],
  attachedFilePaths: ['./32edq', './32edq'],
  library: 'Balluff',
  attributes: {
    manufacturer: 'Company Name',
    manufacturerUri: 'www.company.com',
    deviceClass: 'Device Class',
    model: 'Model',
    productCode: 'Product Code',
    ipCode: 'IP Code',
    material: 'Material',
    length: 0,
    width: 0,
    height: 0,
    weight: 0,
    temperatureMin: 0,
    temperatureMax: 0,
  },
  wires: ['C1', 'C2', 'C3', 'C4'],
  connectors: [
    {
      type: 'M12A3PinFemale',
      pins: [
        {
          //  type: 'PinType',
          name: '1',
          connectedWire: 'C1',
        },
        {
          //  type: 'PinType',
          name: '2',
          connectedWire: 'C2',
        },
        {
          //  type: 'PinType',
          name: '3',
          connectedWire: 'C3',
        },
        {
          //  type: 'PinType',
          name: '4',
          connectedWire: 'C4',
        },
      ],
    },
    {
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
    },
  ],
};
