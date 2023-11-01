import { client } from "@/lib/client";
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

type Props = {
  boardReference: string;
};

export const useBoardQuery = ({ boardReference }: Props) => {
  return useQuery({
    queryKey: ["boards", boardReference],
    queryFn: () => getBoard(boardReference),
  });
};
