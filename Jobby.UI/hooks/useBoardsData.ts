import { useToast } from "@/components/ui/use-toast";
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
      })

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

  async function deleteBoard(BoardId: string) {
    const response = await clientApi.delete(`/board/delete/${BoardId}`);

    return [response, BoardId];
  }

  return useMutation(deleteBoard, {
    onSuccess: ([_, BoardId]) => {
      toast({
        title: "Board delete event.",
        description: "Successfully deleted your board.",
      })

      queryClient.setQueryData(["boards"], (prevData: Board[] | undefined) =>
        prevData?.filter((Board) => Board.id !== BoardId)
      );
    },
    onError: () => {
      toast({
        variant: "destructive",
        title: "Board delete event.",
      })
    }

  });
};

export const useBoardsQuery = (initialBoards?: Board[]) => {
  const clientApi = useClientApi();

  const getBoards = async () => {
    const response = await clientApi.get<Board[]>("/boards");

    return response.data;
  };

  return useQuery<Board[]>({
    queryKey: ["boards"],
    queryFn: getBoards,
    initialData: initialBoards,
    staleTime: Infinity,
    cacheTime: Infinity,
  });
};

export const useBoardQuery = (boardId: string, initialBoard?: Board) => {
  const clientApi = useClientApi();

  const getBoard = async () => {
    const response = await clientApi.get<Board>(`/board/${boardId}`);

    return response.data;
  }

  return useQuery<Board>({
    queryKey: ["board", boardId],
    queryFn: getBoard,
    initialData: initialBoard,
    staleTime: Infinity,
    cacheTime: Infinity,
  });
};
