import { Products } from "./Products"
import { User } from "./Users"

export interface basket {
    id?: number | undefined,
    product: Products
    date: string,
    userKey: string | undefined,
    quantite: number,
    price: number
}