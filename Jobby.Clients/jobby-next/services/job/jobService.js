const host = process.env.APIHOST
const jwt = process.env.JWT

export const getJobById = async (boardId, jobId) => {

  const https = require("https");
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const options = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: 'Bearer ' + jwt
    },
    agent,
  };

  const response = await fetch(
    `${host}/api/board/${boardId}/Job/${jobId}`,
    options
  );

  const result = await response.json();

  return result;
};

export const createJob = async (job) => {
  const options = {
      method: 'POST',
      headers: {
          'Accept': 'text/plain',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + jwt
      },
      body: JSON.stringify(job)
  }

  const response = await fetch(`${host}/api/job/create`, options)

  const result = await response.json()

  return result;
}
