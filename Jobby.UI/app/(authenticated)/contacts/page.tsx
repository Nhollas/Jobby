import { PageContainer } from "components/Common";
import { Board, Contact } from "types";
import { serverClient } from "clients";
import { Contacts } from "components";

export default async function Page() {
  const contacts = await serverClient.get<Contact[]>("/contact/list");

  return (
    <PageContainer>
      <Contacts contacts={contacts} />
    </PageContainer>
  );
}

