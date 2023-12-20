import { photo } from "./Photo"

export interface Member {
    id: number
    userName: string
    age: number
    aka: string
    gender: string
    introduction: string
    lookingFor: string
    interests: string
    city: string
    country: string
    photos: photo[]
    created: Date
    lastActive: Date
    mainPhotoUrl: string
}

