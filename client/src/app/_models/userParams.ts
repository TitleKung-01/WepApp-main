import { User } from "./user"

export class UserParams {
    gender: string
    minAge = 18
    maxAge = 80
    pageNumber = 1
    pageSize = 5
    orderBy = 'lastActive'

    constructor(user: User) {
        if (user.gender === 'non-binary')
            this.gender = 'non-binary'
        else
            this.gender = user.gender === 'male' ? 'female' : 'male'
    }
}