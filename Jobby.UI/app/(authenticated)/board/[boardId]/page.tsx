import { Kanban } from "components/Board";
import { getBoard, getBoardsDictionary } from "lib/board";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const board = await getBoard(boardId);
  const boardsDictionary = await getBoardsDictionary();

  if (!board || !boardsDictionary) {
    return <div>Board not found</div>;
  }

  const { jobLists } = board;

  return (
    <Kanban
      lists={jobLists}
      boardId={board.id}
      boardsDictionary={boardsDictionary}
    />
  );
}
