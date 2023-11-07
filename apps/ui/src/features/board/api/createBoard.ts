import { client } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Board } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const CreateBoardSchema = z.object({
  name: z
    .string()
    .nonempty("Board name must not be empty")
    .min(5, "Board name must be at least 5 characters long")
    .max(50, "Board name must be at most 50 characters long"),
});

export type CreateBoardDTO = z.infer<typeof CreateBoardSchema>;

export async function createBoard(payload: CreateBoardDTO) {
  try {
    const response = await client.post<
      any,
      AxiosResponse<Board>,
      CreateBoardDTO
    >("/board", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useCreateBoard = () => {
  return useMutation({
    onMutate: async () => {
      const previousBoards = queryClient.getQueryData<Board[]>(["boards"]);

      return { previousBoards };
    },
    onError: (_, __, context: any) => {
      if (context?.previousBoards) {
        queryClient.setQueryData(["boards"], context.previousBoards);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["boards"]),
    mutationFn: createBoard,
  });
};
