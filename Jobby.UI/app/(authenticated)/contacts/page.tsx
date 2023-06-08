import { Contact } from "types";
import { Contacts } from "components";
import { serverApi } from "@/lib/clients";

export default async function Page() {
  const url = "/contacts";

  const { data: contacts } = await serverApi.get<Contact[]>(url);

  return <Contacts contacts={contacts} url={url} />;
}
