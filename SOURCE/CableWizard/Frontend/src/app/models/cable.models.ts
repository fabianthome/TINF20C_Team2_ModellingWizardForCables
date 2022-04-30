export const cable = {
  id: '12sadasd',
  name: 'BCC M313-M313-30-300-EX43T2-050',
  attachedImagePaths: ['./32edq', './32edq'],
  attachedFilePaths: ['./32edq', './32edq'],
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
