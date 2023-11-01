import { DeleteBoardModal } from "@/features/board/components/DeleteBoardModal";

export default async function Page({
  params: { boardRef },
}: {
  params: { boardRef: string };
}) {
  return <DeleteBoardModal boardRef={boardRef} />;
}
