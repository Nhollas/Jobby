import { PageContainer } from "components/Common";
import { BoardNavigation } from "./boardNavigation";

export default async function BoardLayout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal?: React.ReactNode;
}) {
  return (
    <PageContainer>
      <BoardNavigation />
      {children}
      {modal}
    </PageContainer>
  );
}
