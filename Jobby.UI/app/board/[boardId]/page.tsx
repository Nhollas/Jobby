import { serverClient } from "../../../clients";
import { Board } from "../../../types";
import { Kanban } from "../../../components/Kanban";
import { BoardDictionaryResponse } from "../../../types/responses/Board";

async function getBoard(boardId: string) {
  const board = await serverClient.get<Board>(`/board/${boardId}`);

  return board;
}

async function getBoardsDictionary() {
  const boardsDictionary = await serverClient.get<BoardDictionaryResponse[]>(
    `/BoardsDictionary`
  );

  return boardsDictionary;
}

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const board = await getBoard(boardId);
  const boardsDictionary = await getBoardsDictionary();

  const { jobLists } = board;

  return (
    <Kanban
      lists={jobLists}
      boardId={board.id}
      boardsDictionary={boardsDictionary}
    />
  );
}
