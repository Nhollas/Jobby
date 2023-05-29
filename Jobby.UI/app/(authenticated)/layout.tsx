import Providers from "../providers";
import { BoardsBar } from "./BoardsBar";

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <Providers>
      <main className="grid min-h-screen lg:grid-cols-[250px,1fr]">
        <BoardsBar />
        {children}
      </main>
    </Providers>
  );
}
