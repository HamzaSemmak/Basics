export interface User {
    id?: number | undefined,
    name: string,
    gender: 'Male' | 'Female',
    email: string,
    password: string | any,
    active: true | false,
    Key: string
}