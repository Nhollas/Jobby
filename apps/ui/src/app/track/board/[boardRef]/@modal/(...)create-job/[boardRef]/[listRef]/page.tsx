import { CreateJobModal } from "@/features/job/components/CreateJobModal";

export default async function Page({
  params: { boardRef, listRef },
}: {
  params: { boardRef: string; listRef: string };
}) {
  return (
    <CreateJobModal
      boardsDictionary={boardDictionaries}
      boardId={selectedBoardId}
      jobListId={selectedJobListId}
    />
  );
}
