import { PageContainer } from "../../../components/Common";
import { BoardNavigation } from "./boardNavigation";

export default async function BoardLayout({ children }) {
  return (
    <PageContainer>
      <BoardNavigation />
      {children}
    </PageContainer>
  );
}
