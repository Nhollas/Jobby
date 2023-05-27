import Providers from "../providers";
import { BoardsBar } from "./BoardsBar";
import { Board } from "types";
import { serverApi } from "@/lib/clients/serverApi";

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { data: boards } = await serverApi.get<Board[]>("/boards");

  return (
    <Providers boards={boards}>
      <main className="grid min-h-screen lg:grid-cols-[250px,1fr]">
        <BoardsBar />
        {children}
      </main>
    </Providers>
  );
}
