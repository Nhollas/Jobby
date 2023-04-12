import { BoardsBar } from "app/(authenticated)/BoardsBar";
import { PageContainer } from "../../../../components/Common";
import { BoardNavigation } from "./boardNavigation";

export default async function BoardLayout({ children }: { children: React.ReactNode }) {
  return (
    <PageContainer>
          <BoardNavigation />
          {children}
    </PageContainer>
  );
}
