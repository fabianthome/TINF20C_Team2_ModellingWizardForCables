export interface ProductDetails {
  id: string
  name: string
  library: string
  attributes: ProductAttributes
  connectors: Connector[]
  wires: Wire[]
}

export interface ProductAttributes {
  manufacturer: string | null
  manufacturerUri: string | null
  deviceClass: string | null
  model: string | null
  productCode: string | null
  ipCode: string | null
  material: string | null
  length: number | null
  width: number | null
  height: number | null
  weight: number | null
  temperatureMin: number | null
  temperatureMax: number | null
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

export type Wire = string
