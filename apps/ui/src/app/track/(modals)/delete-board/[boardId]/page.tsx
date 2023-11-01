import { DeleteBoardModal } from "@/features/board/components/DeleteBoardModal";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  return <DeleteBoardModal boardId={boardId} />;
}
