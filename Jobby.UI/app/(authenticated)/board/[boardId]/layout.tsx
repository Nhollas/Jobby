import { PageContainer } from "../../../../components/Common";
import { BoardInfo } from "./boardInfo";
import { BoardNavigation } from "./boardNavigation";

export default async function BoardLayout({ children }) {
  return (
    <PageContainer>
      <div className="h-full grid grid-cols-6 gap-x-4">
      <BoardInfo />
        <div className="flex flex-col gap-y-4 h-full col-span-5">
          <BoardNavigation />
          {children}
        </div>
      </div>
    </PageContainer>
  );
}
