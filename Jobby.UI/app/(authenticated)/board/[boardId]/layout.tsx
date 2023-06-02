import { BoardNavigation } from "./boardNavigation";

export default async function BoardLayout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal?: React.ReactNode;
}) {
  return (
    <div className="relative h-screen w-[calc(100vw-250px)]">
      {modal}
      <BoardNavigation />
      {children}
    </div>
  );
}
