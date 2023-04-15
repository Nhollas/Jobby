import { PageContainer } from "components/Common";
import { Board } from "types";
import { Boards } from "components/Board/Boards";
import { serverClient } from "clients";
import Link from "next/link";

async function getBoards() {
  const boards = await serverClient.get<Board[]>("/boards");

  return boards;
}

export default async function Page() {
  const boards = await getBoards();

  return (
    <PageContainer small title={"Dashboard"}>
      <Link href="/dashboard/cart/create">Go To Modal...</Link>
      <Boards boards={boards} />
    </PageContainer>
  );
}

