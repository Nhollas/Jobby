import { PageContainer } from "components/Common";
import { Contact } from "types";
import { Contacts } from "components";
import { serverApi } from "@/lib/clients/serverApi";

export default async function Page() {
  const { data: contacts } = await serverApi.get<Contact[]>("/contact/list");

  return (
    <PageContainer>
      <Contacts contacts={contacts} />
    </PageContainer>
  );
}
