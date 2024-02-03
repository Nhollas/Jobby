import { BoardTopNavigation } from "@/features/board";

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
      className="relative h-screen w-screen lg:w-[calc(100vw-250px)]"
    >
      {modal}
      <BoardTopNavigation />
      {children}
    </section>
  );
}
