const host = process.env.APIHOST;

export const moveJobList = async (model, accessToken) => {
  const options = {
    method: "PUT",
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json",
      Authorization: "Bearer " + accessToken,
    },
    body: JSON.stringify(model),
  };

  await fetch(`${host}/api/joblist/move`, options);
};
