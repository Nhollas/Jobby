import { BoardNavigation } from "@/features/board";

export default async function BoardLayout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
}) {
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
