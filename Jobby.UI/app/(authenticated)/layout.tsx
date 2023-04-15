import Providers from "../providers";
import { BoardsBar } from "./BoardsBar";
import { serverClient } from "clients";
import { Board } from "types";

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  const boards = await serverClient.get<Board[]>("/boards");

  return (
    <Providers boards={boards}>
      <main className="grid w-screen md:grid-cols-[250px_calc(100%-250px)]">
        <BoardsBar />
        {children}
      </main>
    </Providers>
  );
}
