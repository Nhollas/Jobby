import { auth } from "@clerk/nextjs";
import { CreateJobModal } from "@/components/modals/CreateJobModal";
import { BoardDictionaryResponse } from "types/responses/Board";
import { serverApi } from "@/lib/clients/serverApi";

export default async function Page({
  params: { selectedBoardId, selectedJobListId },
}: {
  params: { selectedBoardId: string; selectedJobListId: string };
}) {
  const { getToken } = auth();

  const { data: boardDictionaries } = await serverApi.get<
    BoardDictionaryResponse[]
  >("/boardDictionaries", {
    headers: {
      Authorization: `Bearer ${await getToken()}`,
    },
  });

  return (
    <CreateJobModal
      boardsDictionary={boardDictionaries}
      boardId={selectedBoardId}
      jobListId={selectedJobListId}
    />
  );
}
