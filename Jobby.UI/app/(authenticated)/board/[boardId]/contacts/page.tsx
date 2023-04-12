import { serverClient } from "clients";
import { Contact } from "types";
import { Contacts } from "components";

async function getContacts(boardId: string) {
  const contacts = await serverClient.get<Contact[]>(
    `/board/${boardId}/contacts`
  );

  return contacts;
}

export async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const contacts = await getContacts(boardId);

  return <Contacts contacts={contacts} boardId={boardId} />;
}

export default Page;
