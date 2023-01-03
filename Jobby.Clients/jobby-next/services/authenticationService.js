const host = process.env.APIHOST;

export const Login = async (username, password) => {
  const https = require("https");
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const options = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    agent,
    body: JSON.stringify({
      username,
      password,
    }),
  };

  const response = await fetch(`${host}/api/auth/login`, options);

  console.log(response);

  const result = await response.json();

  return result;
};
