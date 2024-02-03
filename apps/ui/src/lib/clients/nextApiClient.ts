import axios from "axios";

export const nextApiClient = axios.create({
  baseURL: "/api/",
  headers: {
    "Content-Type": "application/json",
  },
});