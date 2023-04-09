import { serverClient } from '../../clients';
import { Board } from '../../types';

export const getBoard = async (boardId: string) => {
  const board = await serverClient.get<Board>(`/board/${boardId}`);

  // delay to simulate network latency
  await new Promise((resolve) => setTimeout(resolve, 3000));

  return board;
};