import { DeleteBoardModal } from "@/features/board";

export default async function Page({
  params: { boardRef },
}: {
  params: { boardRef: string };
}) {
  return <DeleteBoardModal boardRef={boardRef} />;
}
