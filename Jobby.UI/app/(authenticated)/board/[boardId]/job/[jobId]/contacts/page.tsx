import { getJobContacts } from "lib/job";
import { Contacts } from "components";

export default async function Page({
  params: { jobId, boardId },
}: {
  params: { jobId: string, boardId: string };
}) {
  const contacts = await getJobContacts(jobId);

  return <Contacts contacts={contacts} boardId={boardId} />;
}
