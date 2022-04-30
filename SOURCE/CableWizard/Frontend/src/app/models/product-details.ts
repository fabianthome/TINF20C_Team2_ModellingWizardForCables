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
  manufacturer: string;
  manufacturerURI: string;
  deviceClass: string;
  model: string;
  productCode: string;
  productInstanceURI: string;
}

export interface AmbientTemperature {
  temperatureMin: number;
  temperatureMax: number;
}

export interface GeneralTechnicalData {
  ambientTemperature: AmbientTemperature;
  iPCode: string;
  material: string;
  weight: number;
  height: number;
  width: number;
  length: number;
}

export interface ProductAttributes {
  identificationData: IdentificationData;
  generalTechnicalData: GeneralTechnicalData;
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
