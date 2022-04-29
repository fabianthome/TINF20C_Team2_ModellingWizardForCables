export interface ProductDetails {
  id: string
  name: string
  attachedImagePaths : string[]
  attachedFilePaths : string[]
  attributes: Object //todo: define more precisely
  connectors: Connector[]
  wires: Wire[]
}

export interface Connector {
  name: string
  pins: Pin[]
}

export interface Pin {
  name: string
  type: string
  connectedWireName: string
}

export interface Wire {
  name: string
}
