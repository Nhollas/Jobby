import "../globals.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import { Poppins } from "@next/font/google";
import Providers from "../providers";
import { BoardsBar } from "./BoardsBar";
import { serverClient } from "clients";
import { Board } from "types";

const poppins = Poppins({
  display: "swap",
  weight: ["100", "200", "300", "400", "500", "600", "700", "800", "900"],
  subsets: ["latin", "latin-ext", "devanagari"],
});

export default async function Layout({ children }: { children: React.ReactNode }) {
  const boards = await serverClient.get<Board[]>("/boards");

  return (
    <html lang="en" className={poppins.className}>
      <head />
      <body>
        <Providers boards={boards}>
          <main>
            <div className="grid h-full grid-cols-6">
              <BoardsBar/>
              <div className="col-span-5 flex h-full flex-col gap-y-4">
                {children}
              </div>
            </div>
          </main>
        </Providers>
      </body>
    </html>
  );
}
