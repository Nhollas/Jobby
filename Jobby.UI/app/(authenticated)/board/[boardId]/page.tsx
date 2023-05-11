import { getAsync } from "@/lib/serverFetch";
import { Kanban } from "components/Board";
import { Board } from "types";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const board = await getAsync<Board>(`/board/${boardId}`);

  if (!board) {
    return <div>Board not found</div>;
  }

  const { jobLists } = board;

  return <Kanban lists={jobLists} boardId={board.id} />;
}
