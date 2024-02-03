import { nextApiClient } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Board } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const UpdateBoardSchema = z.object({
  name: z.string(),
});

export type UpdateBoardDTO = z.infer<typeof UpdateBoardSchema>;

export async function updateBoard(payload: UpdateBoardDTO) {
  try {
    const response = await nextApiClient.put<
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
      if (context?.previousBoards) {
        queryClient.setQueryData(["boards"], context.previousBoards);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["boards"]),
    mutationFn: updateBoard,
  });
};
