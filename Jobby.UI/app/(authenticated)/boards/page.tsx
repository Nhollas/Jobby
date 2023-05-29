import { PageContainer } from "components/Common";
import { Boards } from "components/Board/Boards";
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
