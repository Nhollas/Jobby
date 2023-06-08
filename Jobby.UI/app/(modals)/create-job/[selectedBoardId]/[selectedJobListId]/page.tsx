import { CreateJobModal } from "@/components/modals/CreateJobModal";
import { BoardDictionaryResponse } from "types/responses/Board";
import { serverApi } from "@/lib/clients";

export default async function Page({
  params: { selectedBoardId, selectedJobListId },
}: {
  params: { selectedBoardId: string; selectedJobListId: string };
}) {
  const { data: boardDictionaries } = await serverApi.get<
    BoardDictionaryResponse[]
  >("/boardDictionaries");

  return (
    <CreateJobModal
      boardsDictionary={boardDictionaries}
      boardId={selectedBoardId}
      jobListId={selectedJobListId}
    />
  );
}
