import { nextApiClient } from "@/lib/clients/nextApiClient";
import { Board } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getBoards() {
  try {
    const response = await nextApiClient.get<Board[]>("/boards");

    return response.data;
  } catch (error) {
    console.log("Error fetching boards", error);
    return Promise.reject({});
  }
}

export const useBoardsQuery = () => {
  return useQuery({
    queryKey: ["boards"],
    queryFn: getBoards,
    onSuccess() {},
    onError() {},
  });
};
