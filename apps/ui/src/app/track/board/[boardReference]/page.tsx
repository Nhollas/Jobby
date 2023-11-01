import { Kanban } from "@/features/board";

export default async function Page({
  params: { boardReference },
}: {
  params: { boardReference: string };
}) {
  return <Kanban boardRef={boardReference} />;
}
