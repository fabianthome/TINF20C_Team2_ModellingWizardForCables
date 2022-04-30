export interface ProductDetails {
  id: string;
  name: string;
  attachedImagePaths: string[];
  attachedFilePaths: string[];
  attributes: ProductAttributes;
  connectors: Connector[];
  wires: Wire[];
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

export interface ProductAttributes {
  IdentificationData: IdentificationData;
  GeneralTechnicalData: GeneralTechnicalData;
}

export interface Connector {
  name: string;
  pins: Pin[];
}

export interface Pin {
  name: string;
  type: string;
  connectedWireName: string;
}

export interface Wire {
  name: string;
}
