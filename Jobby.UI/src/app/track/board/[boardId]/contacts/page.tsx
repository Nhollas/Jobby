import { Contact } from "@/types";
import { Contacts } from "@/components";
import { serverApi } from "@/lib/clients";

export default async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const url = `/board/${boardId}/contacts`;
  const { data: contacts } = await serverApi.get<Contact[]>(url);

  return (
    <Contacts
      contacts={contacts}
      boardId={boardId}
      url={url}
      querykeyVariable={url}
    />
  );
}
