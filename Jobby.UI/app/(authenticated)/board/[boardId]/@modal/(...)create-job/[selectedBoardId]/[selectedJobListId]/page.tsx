import { auth } from "@clerk/nextjs";
import { getAsync } from "@/lib/serverFetch";
import { CreateJobModal } from "components/Modals/CreateJobModal";
import { BoardDictionaryResponse } from "types/responses/Board";

export default async function Page({
  params: { selectedBoardId, selectedJobListId },
}: {
  params: { selectedBoardId: string; selectedJobListId: string };
}) {
  const { getToken } = auth();

  const boardDictionaries = await getAsync<BoardDictionaryResponse[]>(
    "/boardDictionaries",
    {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    }
  );

  return (
    <CreateJobModal
      boardsDictionary={boardDictionaries}
      boardId={selectedBoardId}
      jobListId={selectedJobListId}
    />
  );
}
