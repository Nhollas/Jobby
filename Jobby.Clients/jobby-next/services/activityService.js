const host = process.env.APIHOST;

export const activityList = async (boardId, accessToken) => {
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
    `${host}/api/board/${boardId}/activities`,
    options
  );

  const result = await response.json();

  return result;
};

export const updateActivity = async (activity, accessToken) => {
  const https = require("https");
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const options = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    body: JSON.stringify(activity),
    agent,
  };

  await fetch(`${host}/api/activity/update`, options);
};
