import { serverApi } from "@/lib/clients";
import { Board } from "@/types";
import Providers from "../providers";
import { BoardsBar } from "./BoardsBar";
import { Toaster } from "@/components/ui/toaster";
import axios, { AxiosError } from "axios";

async function fetchBoards() {
  try {
    const { data: boards } = await serverApi.get<Board[]>("/boards");

    return boards;
  } catch (error: any | AxiosError) {
    if (axios.isAxiosError(error)) {
      return error;
    }
  }
}

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  const initialBoards = await fetchBoards();

  if (initialBoards instanceof Error) {
    console.log(initialBoards.message);
    return <h1>Temp error penis</h1>;
  } else {
    return (
      <Providers>
        <main className="grid min-h-screen lg:grid-cols-[250px,1fr]">
          <BoardsBar initialBoards={initialBoards} />
          {children}
          <Toaster />
        </main>
      </Providers>
    );
  }
}
