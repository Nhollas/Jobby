import { serverApi } from "@/lib/clients/serverApi";
import { Kanban } from "components/Board";
import { Board } from "types";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const { data: board } = await serverApi.get<Board>(`/board/${boardId}`);
  if (!board) {
    return <div>Board not found</div>;
  }

  const { jobLists } = board;

  return <Kanban lists={jobLists} boardId={board.id} />;
}
