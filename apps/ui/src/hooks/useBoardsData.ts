import { useToast } from "@/components/ui/use-toast";
import { getBoard, GetBoardResponse } from "@/contracts/queries/GetBoard";
import { getBoards, GetBoardsResponse } from "@/contracts/queries/GetBoards";
import { useClientApi } from "@/lib/clients";
import { Board } from "@/types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export const useCreateBoard = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();
  const { toast } = useToast();

  async function createBoard(values: any) {
    return await clientApi.post<any, AxiosResponse<Board>>(
      "/board/create",
      values
    );
  }

  return useMutation(createBoard, {
    onSuccess: ({ data: createdBoard }) => {
      toast({
        title: "Board created successfully.",
        description: "We've created your board for you.",
      });

      queryClient.setQueryData(["boards"], (prevData: Board[] | undefined) => {
        return prevData ? [...prevData, createdBoard] : [createdBoard];
      });
    },
  });
};

export const useUpdateBoard = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();

  async function updateBoard(values: any) {
    return await clientApi.post<any, Board>("/board/update", values);
  }

  return useMutation(updateBoard, {
    onSuccess: (updatedBoard) => {
      queryClient.setQueryData(["boards"], (prevData: Board[] | undefined) => {
        if (prevData) {
          return prevData.map((Board) => {
            if (Board.id === updatedBoard.id) {
              return updatedBoard;
            }
            return Board;
          });
        }
        return [updatedBoard];
      });
    },
  });
};

export const useDeleteBoard = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();
  const { toast } = useToast();

  async function deleteBoard(boardId: string) {
    await clientApi.delete(`/board/delete/${boardId}`);

    return boardId;
  }

  return useMutation(deleteBoard, {
    onSuccess: (boardId) => {
      queryClient.setQueryData(["boards"], (prevData: Board[] | undefined) =>
        prevData?.filter((board) => board.id !== boardId)
      );
    },
    onError: () => {
      toast({
        variant: "destructive",
        title: "Board delete event.",
      });
    },
  });
};

export const useBoardsQuery = (initialBoards?: GetBoardsResponse) => {
  const clientApi = useClientApi();

  return useQuery<GetBoardsResponse | undefined>({
    queryKey: ["boards"],
    queryFn: () => getBoards(clientApi),
    initialData: initialBoards,
    staleTime: Infinity,
    cacheTime: Infinity,
  });
};

export const useBoardQuery = (
  boardId: string,
  initialBoard?: GetBoardResponse
) => {
  const clientApi = useClientApi();

  return useQuery<GetBoardResponse | undefined>({
    queryKey: ["board", boardId],
    queryFn: () => getBoard(boardId, clientApi),
    initialData: initialBoard,
    staleTime: Infinity,
    cacheTime: Infinity,
  });
};
