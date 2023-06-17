import { AxiosInstance } from "axios";

export type GetBoardsResponse = Board[];

type Board = {
  id: string;
  name: string;
  createdDate: Date;
};

export const getBoards = async (client: AxiosInstance) => {
  const response = await client.get<GetBoardsResponse>(`/boards`);

  return response.data;
};
