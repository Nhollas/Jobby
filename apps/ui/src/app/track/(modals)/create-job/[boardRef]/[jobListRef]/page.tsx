import { CreateJobModal } from "@/features/job/components/CreateJobModal";

export default async function Page({
  params: { boardRef, jobListRef },
}: {
  params: { boardRef: string; jobListRef: string };
}) {
  return (
    <CreateJobModal
      boardsDictionary={[]}
      boardRef={boardRef}
      jobListRef={jobListRef}
    />
  );
}