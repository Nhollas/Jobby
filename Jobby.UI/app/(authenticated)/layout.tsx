import Providers from "../providers";
import { BoardsBar } from "./BoardsBar";
import { getAsync } from "lib/serverFetch";
import { Board } from "types";

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  const boards = await getAsync<Board[]>("/boards");

  return (
    <Providers boards={boards}>
      <main className="flex flex-row">
        <BoardsBar />
        {children}
      </main>
    </Providers>
  );
}
