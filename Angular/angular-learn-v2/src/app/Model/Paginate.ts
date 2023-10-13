import { Product } from "./products";

export interface Paginate {
    items: Product[],
    page: number,
    size: number,
    total: number
}