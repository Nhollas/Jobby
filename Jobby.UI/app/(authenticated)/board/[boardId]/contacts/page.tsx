import { Contact } from "types";
import { Contacts } from "components";
import { serverApi } from "@/lib/clients/serverApi";

export async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const { data: contacts } = await serverApi.get<Contact[]>(
    `/board/${boardId}/contacts`
  );

  return <Contacts contacts={contacts} boardId={boardId} />;
}

export default Page;
