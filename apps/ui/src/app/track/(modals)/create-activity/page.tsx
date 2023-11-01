import { CreateActivityModal } from "@/features/activity";

export default async function Page({
  searchParams,
}: {
  searchParams: { boardRef: string; filter: string; jobRef?: string };
}) {
  const { filter, jobRef, boardRef } = searchParams;

  return (
    <CreateActivityModal boardRef={boardRef} jobRef={jobRef} filter={filter} />
  );
}
