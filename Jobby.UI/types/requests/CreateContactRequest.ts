export type CreateContactRequest = {
    "boardId": string,
    "jobIds": string[],
    "firstName": string,
    "lastName": string,
    "jobTitle": string,
    "location": string,
    "socials": Social,
    "emails": Email[],
    "phones": Phone[],
    "companies": Company[]
}

export interface Base {
    "id": string,
    "value": string,
    "type"?: number
}

type Company = Base & {
    "name": string
}

type Email = Base & {
    "name": string,
}

type Phone = Base & {
    "number": string,
}

type Social = {
    "twitterUrl": string,
    "facebookUrl": string,
    "linkedInUrl": string,
    "githubUrl": string
}


