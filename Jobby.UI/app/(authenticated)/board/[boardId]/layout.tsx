import { BoardNavigation } from "./boardNavigation";

export default async function BoardLayout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal?: React.ReactNode;
}) {
  return (
    <div>
      <BoardNavigation />
      {children}
      {modal}
    </div>
  );
}
