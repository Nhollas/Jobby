import { Boards } from "@/components";
import { getBoards } from "@/contracts/queries/GetBoards";
import { serverApi } from "@/lib/clients";

export default async function Page() {
  const boards = await getBoards(serverApi);

  return <Boards initialBoards={boards} />;
}
