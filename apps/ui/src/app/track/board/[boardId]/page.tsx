import { serverApi } from "@/lib/clients";
import { Kanban } from "@/components/Kanban";
import { getBoard } from "@/contracts/queries/GetBoard";
import { redirect } from "next/navigation";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const board = await getBoard(boardId, serverApi);

  if (!board) {
    return redirect("/track/boards");
  }

  return <Kanban initialBoard={board} />;
}
