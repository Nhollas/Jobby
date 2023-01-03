const host = process.env.APIHOST;

export const boardList = async (accessToken) => {
  const https = require("https");
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const options = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    agent,
  };

  const response = await fetch(`${host}/api/boards`, options);

  const result = await response.json();

  return result;
};

export const getBoardById = async (boardId, accessToken) => {
  const https = require("https");
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const options = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    agent,
  };

  const response = await fetch(`${host}/api/board/${boardId}`, options);

  const result = await response.json();

  return result;
};

export const createBoard = async (board, accessToken) => {
  const options = {
    method: "POST",
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    body: JSON.stringify(board),
  };

  const response = await fetch(`${host}/api/board/create`, options);

  const result = await response.json();

  return result;
};

export const deleteBoard = async (boardId, accessToken) => {
  const options = {
    method: "DELETE",
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
  };

  await fetch(`${host}/api/board/delete/${boardId}`, options);
};

export const updateBoard = async (boardId, boardName, accessToken) => {
  const options = {
    method: "PUT",
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    body: JSON.stringify({ boardId, boardName }),
  };

  await fetch(`${host}/api/board/update`, options);
};
