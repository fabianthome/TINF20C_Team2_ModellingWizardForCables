export const cable = {
  id: '12sadasd',
  name: 'BCC M313-M313-30-300-EX43T2-050',
  attachedImagePaths: ['./32edq', './32edq'],
  attachedFilePaths: ['./32edq', './32edq'],
  library: 'Balluff',
  attributes: {
    manufacturer: 'Balluff',
    manufacturerUri: 'www.balluff.com',
    deviceClass: 'Double-Ended Cordsets',
    model: 'BCC M415-M415-3A-312-PX0534-035',
    productCode: 'BCC0KZ3',
    ipCode: 'IP67',
    material: 'Zinc',
    length: 224,
    width: 68,
    height: 38,
    weight: 695,
    temperatureMin: -5,
    temperatureMax: 70,
  },
  wires: ['C1', 'C2', 'C3', 'C4'],
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
