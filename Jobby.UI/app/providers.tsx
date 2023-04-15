"use client";

import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import NavigationContext from "contexts/NavigationContext";
import { useCycle } from "framer-motion";
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
  const [isOpen, toggleOpen] = useCycle(false, true);

  return (
    <SessionProvider>
      <NavigationContext.Provider value={{ isOpen, toggleOpen }}>
        <BoardsAndJobsContext.Provider value={{ boards, setBoards }}>
          <ModalProvider>{children}</ModalProvider>
        </BoardsAndJobsContext.Provider>
      </NavigationContext.Provider>
    </SessionProvider>
  );
}
