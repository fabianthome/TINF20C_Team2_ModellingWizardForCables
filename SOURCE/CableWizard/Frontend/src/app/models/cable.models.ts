export const cable = {
  id: 1,
  name: 'BCC M313-M313-30-300-EX43T2-050',
  attributes: {
    IdentificationData: {
      Manufacturer: 'Balluff',
      ManufacturerURI: 'www.balluff.com',
      DeviceClass: 'Double-Ended Cordsets',
      Model: 'BCC M415-M415-3A-312-PX0534-035',
      ProductCode: 'BCC0KZ3',
      ProductInstanceURI:
        'https://www.balluff.com/local/us/productfinder/#/ca/A0011/product/G1103/variant/PV8754556?bas_connector_coding_01=260032&amp;cal_connector_head1=-1301766766&amp;cal_connector_head2=1196614099',
    },
    GeneralTechnicalData: {
      AmbientTemperature: {
        TemperatureMin: -5,
        TemperatureMax: 70,
      },
      IPCode: 'IP67',
      Material: 'Zinc',
      Weight: 695,
      Height: 38,
      Widht: 68,
      Length: 224,
    },
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

export interface IdentificationData {
  Manufacturer: string;
  ManufacturerURI: string;
  DeviceClass: string;
  Model: string;
  ProductCode: string;
  ProductInstanceURI: string;
}

export interface AmbientTemperature {
  TemperatureMin: number;
  TemperatureMax: number;
}

export interface GeneralTechnicalData {
  AmbientTemperature: AmbientTemperature;
  IPCode: string;
  Material: string;
  Weight: number;
  Height: number;
  Widht: number;
  Length: number;
}

export interface Attributes {
  IdentificationData: IdentificationData;
  GeneralTechnicalData: GeneralTechnicalData;
}

export interface Cable {
  attributes: Attributes;
  cableId: number;
  cableName: string;
  wires: string[];
  connectors: Connector[];
  connectorType: string;
  pins: Pin[];
}
