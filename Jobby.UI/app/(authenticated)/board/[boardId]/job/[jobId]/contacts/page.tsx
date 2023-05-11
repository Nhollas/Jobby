import { Contacts } from "components";
import { getAsync } from "@/lib/serverFetch";
import { Contact } from "types";
import { auth } from "@clerk/nextjs";

export default async function Page({
  params: { jobId, boardId },
}: {
  params: { jobId: string; boardId: string };
}) {
  const { getToken } = auth();

  const contacts = await getAsync<Contact[]>(`/job/${jobId}/contacts`, {
    headers: {
      Authorization: `Bearer ${await getToken()}`,
    },
  });

  return <Contacts contacts={contacts} boardId={boardId} />;
}
