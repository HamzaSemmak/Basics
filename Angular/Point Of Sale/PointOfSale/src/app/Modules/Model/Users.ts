export interface User {
    id?: number | undefined,
    name: string,
    gender: 'Male' | 'Female',
    email: string,
    password: string | any,
    Key: string,
    role: string
}