import { TypesToasts as type } from "./TypeToast";

export interface Toasts {
    title: string,
    content: string,
    show?: boolean,
    type?: type,
    progress?: string
} 