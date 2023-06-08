import { CreateActivityModal } from "@/components/modals/CreateActivityModal";
import { serverApi } from "@/lib/clients/serverApi";
import { Job } from "@/types";

export default async function Page({
  searchParams,
}: {
  searchParams: { boardId: string; activityCategory: string | undefined };
}) {
  const { data: jobs } = await serverApi.get<Job[]>("/jobs");
  const { activityCategory, boardId } = searchParams;

  return <CreateActivityModal jobs={jobs} boardId={boardId} />;
}
