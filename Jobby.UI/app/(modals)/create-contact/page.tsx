import { getAsync } from "@/lib/serverFetch";
import { Board, Job } from "@/types";
import { CreateContactModal } from "components/Modals/CreateContactModal";

export default async function Page() {
  const boards = await getAsync<Board[]>("/boards");
  const jobs = await getAsync<Job[]>("/jobs");

  return <CreateContactModal boards={boards} jobs={jobs} />;
}
