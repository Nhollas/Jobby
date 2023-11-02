import { KanbanWrapper } from "@/features/board";

export default async function Page({
  params: { boardReference },
}: {
  params: { boardReference: string };
}) {
  return <KanbanWrapper boardRef={boardReference} />;
}
