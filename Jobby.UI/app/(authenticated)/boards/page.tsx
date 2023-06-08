import { Boards } from "@/components";
import { serverApi } from "@/lib/clients";
import { Board } from "@/types";

async function fetchBoards() {
  try {
    const { data: boards } = await serverApi.get<Board[]>("/boards");

    return boards;
  } catch (error) {
    console.error(error);
  }
}

export default async function Page() {
  const boards = await fetchBoards();

  return <Boards initialBoards={boards} />;
}
