import { deleteAsync } from "@/lib/serverFetch";
import { DeleteBoardModal } from "components/Modals/DeleteBoardModal";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  return <DeleteBoardModal boardId={boardId} />;
}
