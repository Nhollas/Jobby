import { auth } from "@clerk/nextjs";
import { getAsync } from "app/serverClient";
import { Kanban } from "components/Board";
import { Board } from "types";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const { getToken } = auth();
  const board = await getAsync<Board>(`/board/${boardId}`, {
    headers: {
      Authorization: `Bearer ${await getToken()}`,
    },
  });

  if (!board) {
    return <div>Board not found</div>;
  }

  const { jobLists } = board;

  return <Kanban lists={jobLists} boardId={board.id} />;
}
