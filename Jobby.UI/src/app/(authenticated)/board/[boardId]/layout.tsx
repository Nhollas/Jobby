import { BoardNavigation } from "@/components/BoardNavigation";
import { getBoards } from "@/contracts/queries/GetBoards";
import { serverApi } from "@/lib/clients";

export default async function BoardLayout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
}) {
  const initialBoards = await getBoards(serverApi);

  return (
    <section
      id="BoardLayout"
      className="relative h-screen w-screen md:w-[calc(100vw-250px)]"
    >
      {modal}
      <BoardNavigation initialBoards={initialBoards} />
      {children}
    </section>
  );
}
