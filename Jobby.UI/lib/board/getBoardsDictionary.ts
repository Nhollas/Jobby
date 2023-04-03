import { serverClient } from '../../clients';
import { BoardDictionaryResponse } from '../../types/responses/Board';

export const getBoardsDictionary = async () => {
  const boardsDictionary = await serverClient.get<BoardDictionaryResponse[]>(
    `/BoardsDictionary`
  );

  return boardsDictionary;
};