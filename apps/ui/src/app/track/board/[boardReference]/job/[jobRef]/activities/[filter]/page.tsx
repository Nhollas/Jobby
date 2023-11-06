import { ActivitiesWrapper } from "@/features/activity";

export default async function Page({
  params: { jobRef, boardReference, filter },
}: {
  params: { jobRef: string; boardReference: string; filter: string };
}) {
  return (
    <ActivitiesWrapper
      boardRef={boardReference}
      jobRef={jobRef}
      filter={filter}
    />
  );
}
