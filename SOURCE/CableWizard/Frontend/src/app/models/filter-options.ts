import {ProductDetails} from "./product-details";

export class FilterOptions {
  queryText: string = ""
  width = new NumberRangeFilter()
  height = new NumberRangeFilter()
  length = new NumberRangeFilter()
  weight = new NumberRangeFilter()
  temperature = new NumberRangeFilter()

  apply(products: ProductDetails[]): ProductDetails[] {
    return products.filter(product => {

      if (!this.width.allows(product.attributes.width)) return false
      if (!this.height.allows(product.attributes.height)) return false
      if (!this.length.allows(product.attributes.length)) return false
      if (!this.weight.allows(product.attributes.weight)) return false
      if (!this.temperature.allows(product.attributes.temperatureMin)) return false
      if (!this.temperature.allows(product.attributes.temperatureMax)) return false

      return (product.name.includes(this.queryText));
    });
  }
}

export class NumberRangeFilter {
  min: number | null = null
  max: number | null = null
  acceptNull: boolean = true

  allows(attribute: number | null): boolean {
    if (attribute == null) return this.acceptNull
    if (this.min != null && attribute < this.min) return false
    if (this.max != null && attribute > this.max) return false // noinspection RedundantIfStatementJS

    return true
  }
}


