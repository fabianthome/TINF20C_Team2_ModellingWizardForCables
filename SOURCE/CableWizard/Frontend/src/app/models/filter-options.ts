import {ProductDetails} from "./product-details";

export interface FilterOptions {
  queryText: string
  widthMin: number | null
  widthMax: number | null
  heightMin: number | null
  heightMax: number | null
  lengthMin: number | null
  lengthMax: number | null
  weightMin: number | null
  weightMax: number | null
  temperatureMin: number | null
  temperatureMax: number | null
}

export function filterProducts(products: ProductDetails[], filter: FilterOptions) {
  return products.filter(product => {

    if (!isInRange(product.attributes.length, filter.lengthMin, filter.lengthMax)) return false;
    if (!isInRange(product.attributes.width, filter.widthMin, filter.widthMax)) return false;
    if (!isInRange(product.attributes.height, filter.heightMin, filter.heightMax)) return false;

    return (product.name.includes(filter.queryText));
  });
}

function isInRange(attribute: number | null, min: number | null, max: number | null) {
  if (attribute != null) {
    if (min != null && attribute < min) return false
    if (max != null && attribute > max) return false
  }
  return true
}
