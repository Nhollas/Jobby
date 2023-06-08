import { Contacts } from "components";
import { Contact } from "types";
import { serverApi } from "@/lib/clients";

export default async function Page({
  params: { jobId, boardId },
}: {
  params: { jobId: string; boardId: string };
}) {
  const url = `/job/${jobId}/contacts`;
  const { data: contacts } = await serverApi.get<Contact[]>(url);

  return (
    <Contacts
      contacts={contacts}
      boardId={boardId}
      jobId={jobId}
      url={url}
      querykeyVariable={url}
    />
  );
}
