const host = process.env.APIHOST
const jwt = process.env.JWT

export const boardList = async () => {

    const https = require("https");
    const agent = new https.Agent({
    rejectUnauthorized: false
    })

    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + jwt
        },
        agent 
    }

    const response = await fetch(`${host}/api/boards`, options)

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
            'Authorization': 'Bearer ' + jwt
        },
        agent
        
    }

    const response = await fetch(`${host}/api/board/${boardId}`, options)

    const result = await response.json()

    return result;
}

export const createBoard = async (board) => {
    const options = {
        method: 'POST',
        headers: {
            'Accept': 'text/plain',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + jwt
        },
        body: JSON.stringify(board)
    }

    const response = await fetch(`${host}/api/board/create`, options)

    const result = await response.json()

    return result;
}

export const deleteBoard = async (boardId) => {
    const options = {
        method: 'DELETE',
        headers: {
            'Accept': 'text/plain',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + jwt
        }
    }

    await fetch(`${host}/api/board/delete/${boardId}`, options)
}

export const updateBoard = async (boardId, boardName) => {

    const options = {
        method: 'PUT',
        headers: {
            'Accept': 'text/plain',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + jwt
        },
        body: JSON.stringify({ boardId, boardName })
    }

    console.log(options.body);

    await fetch(`${host}/api/board/update`, options)
}