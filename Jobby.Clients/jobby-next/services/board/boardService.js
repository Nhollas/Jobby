export const boardList = async () => {

    const https = require("https");
    const agent = new https.Agent({
    rejectUnauthorized: false
    })

    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMzdmMzIyZC0zMGRkLTQ5NTAtYjQwMC0xZjI1OTA3NDE4MGYiLCJ1bmlxdWVfbmFtZSI6IlRlc3QiLCJqdGkiOiJjNzI4Y2RmYi1lMWI0LTQ2MTYtODA2YS1hYjViMGI1NTJlYjEiLCJleHAiOjE2NjY2MjQ4OTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjYwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo2MDAxIn0.lfnOD93iu9qZl1qAyvN2E6AFMdLkDMqtnoCokkc2YSo'
        },
        agent
        
    }

    const response = await fetch(`${process.env.apiHost}/api/boards`, options)

    const result = await response.json()

    return result;
}

export const getBoardById = async (boardId) => {
    const https = require("https");
    const agent = new https.Agent({
    rejectUnauthorized: false
    })

    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMzdmMzIyZC0zMGRkLTQ5NTAtYjQwMC0xZjI1OTA3NDE4MGYiLCJ1bmlxdWVfbmFtZSI6IlRlc3QiLCJqdGkiOiJjNzI4Y2RmYi1lMWI0LTQ2MTYtODA2YS1hYjViMGI1NTJlYjEiLCJleHAiOjE2NjY2MjQ4OTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjYwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo2MDAxIn0.lfnOD93iu9qZl1qAyvN2E6AFMdLkDMqtnoCokkc2YSo'
        },
        agent
        
    }

    const response = await fetch(`${process.env.apiHost}/api/board/${boardId}`, options)

    const result = await response.json()

    return result;
}