import { serverClient } from '../../clients';
import { Board } from '../../types';

export const getBoard = async (boardId: string) => {
  const board = await serverClient.get<Board>(`/board/${boardId}`);


  await new Promise((resolve) => setTimeout(resolve, 5000));

  return board;
};