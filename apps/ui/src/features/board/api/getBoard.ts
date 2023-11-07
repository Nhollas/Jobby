import { client } from "@/lib/clients";
import { Board } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getBoard(boardReference: string) {
  try {
    const response = await client.get<Board>(`/board/${boardReference}`);

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useBoardQuery = (boardReference: string) => {
  return useQuery({
    queryKey: ["boards", boardReference],
    queryFn: () => getBoard(boardReference),
  });
};
