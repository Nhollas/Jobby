"use client";

import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import { SessionProvider } from "next-auth/react";
import { useState } from "react";
import { Board } from "types";
import { ModalProvider } from "../contexts/ModalContext";

export default function Providers({
  children,
  boards: initialBoards,
}: {
  children: React.ReactNode;
  boards: Board[];
}) {
  const [boards, setBoards] = useState<Board[]>(initialBoards);

  return (
    <SessionProvider>
      <BoardsAndJobsContext.Provider value={{ boards, setBoards }}>
      <ModalProvider>{children}</ModalProvider>
      </BoardsAndJobsContext.Provider>
    </SessionProvider>
  );
}
