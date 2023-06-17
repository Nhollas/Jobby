import { serverApi } from "@/lib/clients";
import { Kanban } from "@/components/Kanban";
import { getBoard } from "@/contracts/queries/GetBoard";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const board = await getBoard(boardId, serverApi);
  if (!board) {
    return <div>Board not found</div>;
  }

  return <Kanban initialBoard={board} />;
}
