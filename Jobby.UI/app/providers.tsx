"use client";

import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import NavigationContext from "contexts/NavigationContext";
import { useCycle } from "framer-motion";
import { SessionProvider } from "next-auth/react";
import { useState } from "react";
import { Board } from "types";

const Providers = ({
  children,
  boards: initialBoards,
}: {
  children: React.ReactNode;
  boards?: Board[];
}) => {
  const [boards, setBoards] = useState<Board[]>(initialBoards || []);
  const [isOpen, toggleOpen] = useCycle(false, true);

  return (
    <SessionProvider>
      <NavigationContext.Provider value={{ isOpen, toggleOpen }}>
        <BoardsAndJobsContext.Provider value={{ boards, setBoards }}>
          {children}
        </BoardsAndJobsContext.Provider>
      </NavigationContext.Provider>
    </SessionProvider>
  );
}

export default Providers;
