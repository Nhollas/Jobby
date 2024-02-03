import { nextApiClient } from "@/lib/clients/nextApiClient";
import { queryClient } from "@/lib/react-query";
import { Board } from "@/types";
import { useMutation } from "@tanstack/react-query";

export async function deleteBoard(boardReference: string) {
  try {
    await nextApiClient.delete(`/board/${boardReference}`);
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useDeleteBoard = () => {
  return useMutation({
    onMutate: async (boardReference) => {
      await queryClient.cancelQueries(["boards"]);

      const previousBoards = queryClient.getQueryData<Board[]>(["boards"]);

      queryClient.setQueryData(
        ["boards"],
        previousBoards?.filter((board) => board.reference !== boardReference)
      );

      return { previousBoards };
    },
    onSuccess: () => queryClient.invalidateQueries(["boards"]),
    onError: (_, __, context: any) => {
      if (context?.previousBoards) {
        queryClient.setQueryData(["boards"], context.previousBoards);
      }
    },
    mutationFn: deleteBoard,
  });
};
