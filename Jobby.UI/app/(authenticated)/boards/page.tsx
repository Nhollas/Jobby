import { PageContainer } from "components/Common";
import { Board } from "types";
import { Boards } from "components/Board/Boards";
import { serverClient } from "clients";

async function getBoards() {
  const boards = await serverClient.get<Board[]>("/boards");

  return boards;
}

export default async function Page() {
  return (
    <PageContainer>
      <Boards />
    </PageContainer>
  );
}

