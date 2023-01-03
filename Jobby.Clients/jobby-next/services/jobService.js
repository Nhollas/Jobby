const host = process.env.APIHOST;

export const getJobById = async (boardId, jobId, accessToken) => {
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

  const response = await fetch(
    `${host}/api/board/${boardId}/Job/${jobId}`,
    options
  );

  const result = await response.json();

  return result;
};

export const createJob = async (job, accessToken) => {
  const options = {
    method: "POST",
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    body: JSON.stringify(job),
  };

  const response = await fetch(`${host}/api/job/create`, options);

  const result = await response.json();

  return result;
};

export const moveJob = async (model, accessToken) => {
  const options = {
    method: "PUT",
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    body: JSON.stringify(model),
  };

  await fetch(`${host}/api/job/move`, options);
};
