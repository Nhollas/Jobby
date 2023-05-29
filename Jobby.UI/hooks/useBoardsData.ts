import { clientApi } from "@/lib/clients/clientApi";
import { Board } from "@/types";
import { useAuth } from "@clerk/nextjs";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export const useCreateBoard = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();

  async function createBoard(values: any) {
    return await clientApi.post<any, AxiosResponse<Board>>(
      "/board/create",
      values,
      {
        headers: {
          Authorization: `Bearer ${await getToken()}`,
        },
      }
    );
  }

  return useMutation(createBoard, {
    onSuccess: ({ data: createdBoard }) => {
      queryClient.setQueryData(["boards"], (prevData: Board[] | undefined) => {
        return prevData ? [...prevData, createdBoard] : [createdBoard];
      });
    },
  });
};

export const useUpdateBoard = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();

  async function updateBoard(values: any) {
    return await clientApi.post<any, Board>("/board/update", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });
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
  const { getToken } = useAuth();

  async function deleteBoard(BoardId: string) {
    const response = await clientApi.delete(`/board/delete/${BoardId}`, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    return [response, BoardId];
  }

  return useMutation(deleteBoard, {
    onSuccess: ([_, BoardId]) => {
      queryClient.setQueryData(["boards"], (prevData: Board[] | undefined) =>
        prevData?.filter((Board) => Board.id !== BoardId)
      );
    },
  });
};

export const useBoardsQuery = (initialBoards?: Board[]) => {
  const { getToken } = useAuth();

  const getBoards = async () => {
    const response = await clientApi.get<Board[]>("/boards", {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    return response.data;
  };

  return useQuery<Board[]>({
    queryKey: ["boards"],
    queryFn: getBoards,
    initialData: initialBoards,
  });
};
