export type CreateContactRequest = {
    "boardId": string,
    "jobIds": [
      string
    ],
    "firstName": string,
    "lastName": string,
    "jobTitle": string,
    "location": string,
    "socials": {
      "twitterUrl": string,
      "facebookUrl": string,
      "linkedInUrl": string,
      "githubUrl": string
    },
    "emails": Email[],
    "phones": Phone[],
    "companies": Company[]
  }

type Company = {
    "id": string,
    "name": string,
    "contactId": string
}

type Email = {
    "id": string,
    "name": string,
    "contactId": string
}

type Phone = {
    "id": string,
    "number": string,
    "type": number,
    "contactId": string
}


