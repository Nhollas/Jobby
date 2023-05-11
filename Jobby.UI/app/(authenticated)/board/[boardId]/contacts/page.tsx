import { getAsync } from "@/lib/serverFetch";
import { Contact } from "types";
import { Contacts } from "components";
import { auth } from "@clerk/nextjs";

export async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const { getToken } = auth();

  const contacts = await getAsync<Contact[]>(`/board/${boardId}/contacts`, {
    headers: {
      Authorization: `Bearer ${await getToken()}`,
    },
  });

  return <Contacts contacts={contacts} boardId={boardId} />;
}

export default Page;
