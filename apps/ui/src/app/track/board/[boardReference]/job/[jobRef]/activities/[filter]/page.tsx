import { Activities } from "@/features/activity";

export default async function Page({
  params: { jobRef, boardRef, filter },
}: {
  params: { jobRef: string; boardRef: string; filter: string };
}) {
  return <Activities boardRef={boardRef} jobRef={jobRef} filter={filter} />;
}
