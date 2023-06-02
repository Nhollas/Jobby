import { DeleteBoardModal } from "@/components/modals/DeleteBoardModal";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  return <DeleteBoardModal boardId={boardId} />;
}
