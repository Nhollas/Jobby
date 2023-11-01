import { getBoards } from "@/features/board/api";
import { BoardNavigation } from "@/features/board/components";

export default async function BoardLayout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
}) {
  const initialBoards = await getBoards();

  return (
    <section
      id="BoardLayout"
      className="relative h-screen w-screen md:w-[calc(100vw-250px)]"
    >
      {modal}
      <BoardNavigation />
      {children}
    </section>
  );
}
