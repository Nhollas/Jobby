import { Activities } from "@/features/activity";

export default async function Page({
  params: { boardRef, filter },
}: {
  params: { boardRef: string; filter: string };
}) {
  return <Activities filter={filter} boardRef={boardRef} />;
}
