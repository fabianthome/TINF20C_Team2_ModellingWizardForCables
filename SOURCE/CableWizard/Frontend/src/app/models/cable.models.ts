export const cable = {
  id: '12sadasd',
  name: 'BCC M313-M313-30-300-EX43T2-050',
  attachedImagePaths: ['./32edq', './32edq'],
  attachedFilePaths: ['./32edq', './32edq'],
  attributes: {
    identificationData: {
      manufacturer: 'Balluff',
      manufacturerURI: 'www.balluff.com',
      deviceClass: 'Double-Ended Cordsets',
      model: 'BCC M415-M415-3A-312-PX0534-035',
      productCode: 'BCC0KZ3',
      productInstanceURI:
        'https://www.balluff.com/local/us/productfinder/#/ca/A0011/product/G1103/variant/PV8754556?bas_connector_coding_01=260032&amp;cal_connector_head1=-1301766766&amp;cal_connector_head2=1196614099',
    },
    generalTechnicalData: {
      ambientTemperature: {
        temperatureMin: -5,
        temperatureMax: 70,
      },
      iPCode: 'IP67',
      material: 'Zinc',
      weight: 695,
      height: 38,
      width: 68,
      length: 224,
    },
  },
  wires: [{ name: 'C1' }, { name: 'C2' }, { name: 'C3' }, { name: 'C4' }],
  connectors: [
    {
      name: 'M12A3PinFemale',
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
    },
  ],
};
