import Providers from "../providers";
import { BoardsBar } from "./BoardsBar";
import { getAsync } from "app/serverClient";
import { Board } from "types";
import { auth } from "@clerk/nextjs";

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { getToken } = auth();
  const boards = await getAsync<Board[]>("/boards", {
    headers: { Authorization: `Bearer ${await getToken()}` },
  });

  return (
    <Providers boards={boards}>
      <main className="grid w-screen md:grid-cols-[250px_calc(100%-250px)]">
        <BoardsBar />
        {children}
      </main>
    </Providers>
  );
}
