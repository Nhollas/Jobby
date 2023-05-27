import { Contacts } from "components";
import { Contact } from "types";
import { serverApi } from "@/lib/clients/serverApi";

export default async function Page({
  params: { jobId, boardId },
}: {
  params: { jobId: string; boardId: string };
}) {
  const { data: contacts } = await serverApi.get<Contact[]>(
    `/job/${jobId}/contacts`
  );

  return <Contacts contacts={contacts} boardId={boardId} />;
}
