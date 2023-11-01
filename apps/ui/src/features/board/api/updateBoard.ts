import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Board } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export type UpdateBoardDTO = {
  name: string;
};

export async function updateBoard(payload: UpdateBoardDTO) {
  try {
    const response = await client.put<
      any,
      AxiosResponse<Board>,
      UpdateBoardDTO
    >("/board", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useUpdateBoard = () => {
  return useMutation({
    onMutate: async () => {
      const previousBoards = queryClient.getQueryData<Board[]>(["boards"]);

      return { previousBoards };
    },
    onError: (error, __, context: any) => {
      console.error(error);

      if (context?.previousBoards) {
        queryClient.setQueryData(["boards"], context.previousBoards);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["boards"]),
    mutationFn: updateBoard,
  });
};
