import { serverClient } from "../../../../../../clients";
import { Contact } from "../../../../../../types";
import { Contacts } from "../../../contacts/contacts";

async function getContacts(jobId: string) {
  const contacts = await serverClient.get<Contact[]>(`/job/${jobId}/contacts`);

  return contacts;
}

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const contacts = await getContacts(jobId);

  return <Contacts contacts={contacts} />;
}
