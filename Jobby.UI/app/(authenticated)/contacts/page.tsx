import { PageContainer } from "components/Common";
import { Contact } from "types";
import { Contacts } from "components";
import { serverApi } from "@/lib/clients/serverApi";

export default async function Page() {
  const url = "/contacts";

  const { data: contacts } = await serverApi.get<Contact[]>(url);

  return (
    <PageContainer>
      <Contacts contacts={contacts} url={url} />
    </PageContainer>
  );
}
