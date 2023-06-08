import { serverApi } from "@/lib/clients";
import { Board, Job } from "@/types";
import { CreateContactModal } from "@/components/modals/CreateContactModal";

export default async function Page() {
  const { data: boards } = await serverApi.get<Board[]>("/boards");
  const { data: jobs } = await serverApi.get<Job[]>("/jobs");

  return <CreateContactModal boards={boards} jobs={jobs} />;
}
