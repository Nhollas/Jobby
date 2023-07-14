import { serverApi } from "@/lib/clients";
import { getContact } from "@/contracts/queries/GetContact";
import { Contact } from "@/components/Contact";
import { getBoards } from "@/contracts/queries/GetBoards";
import { getJobs } from "@/contracts/queries/GetJobs";

export default async function Page({
  params: { contactId },
}: {
  params: { contactId: string };
}) {
  const contact = await getContact(contactId, serverApi);
  const boards = await getBoards(serverApi);
  const jobs = await getJobs(serverApi);

  if (!contact) {
    return <div>Board not found</div>;
  }

  return <Contact contact={contact} boards={boards} jobs={jobs} />;
}
