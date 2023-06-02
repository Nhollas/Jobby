import { PageContainer } from "@/components/PageContainer";
import { Boards } from "@/components";
import { serverApi } from "@/lib/clients/serverApi";
import { Board } from "@/types";

export default async function Page() {
  const { data: boards } = await serverApi.get<Board[]>("/boards");

  return (
    <PageContainer>
      <Boards boards={boards} />
    </PageContainer>
  );
}
