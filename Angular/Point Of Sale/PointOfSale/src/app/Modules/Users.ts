export interface User {
    id?: number,
    name: string,
    gender: 'Male' | 'Female',
    email: string,
    password: string,
    active: true | false,
    Key: string
}