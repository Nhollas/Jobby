import {
  ActivityFilter,
  CreateActivityModal,
} from "@/components/modals/CreateActivityModal";
import { serverApi } from "@/lib/clients";
import { Board, Job } from "@/types";

export default async function Page({
  searchParams,
}: {
  searchParams: { boardId: string; filter: ActivityFilter; jobId?: string };
}) {
  const { data: jobs } = await serverApi.get<Job[]>("/jobs");
  const { data: board } = await serverApi.get<Board>(
    `/board/${searchParams.boardId}`
  );
  const { filter, jobId } = searchParams;

  return (
    <CreateActivityModal
      jobs={jobs}
      board={board}
      jobId={jobId}
      filter={filter}
    />
  );
}
