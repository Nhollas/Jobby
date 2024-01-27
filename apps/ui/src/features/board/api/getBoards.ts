import { client } from "@/lib/clients";
import { Board } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getBoards() {
  try {
    const response = await client.get<Board[]>("/boards");

    return response.data;
  } catch (error) {
    return Promise.reject({});
  }
}

export const useBoardsQuery = () => {
  return useQuery({
    queryKey: ["boards"],
    queryFn: getBoards,
    onSuccess() {},
    onError() {},
    useErrorBoundary: true,
  });
};
