import { PageContainer } from "components/Common";
import { Contact } from "types";
import { getAsync } from "app/serverClient";
import { Contacts } from "components";
import { auth } from "@clerk/nextjs";

export default async function Page() {
  const { getToken } = auth();
  const contacts = await getAsync<Contact[]>("/contact/list", {
    headers: {
      Authorization: `Bearer ${await getToken()}`,
    },
  });

  return (
    <PageContainer>
      <Contacts contacts={contacts} />
    </PageContainer>
  );
}
