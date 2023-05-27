"use client";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import NavigationContext from "contexts/NavigationContext";
import { useCycle } from "framer-motion";
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
  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <NavigationContext.Provider value={{ isOpen, toggleOpen }}>
        <BoardsAndJobsContext.Provider value={{ boards, setBoards }}>
          {children}
        </BoardsAndJobsContext.Provider>
      </NavigationContext.Provider>
    </QueryClientProvider>
  );
};

export default Providers;
