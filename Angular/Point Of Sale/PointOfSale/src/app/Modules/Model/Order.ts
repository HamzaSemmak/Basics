import { basket } from "./basket";

export interface Orders {
    id?: number | undefined,
    basket: basket[],
    date: string,
    userKey: string | undefined
}