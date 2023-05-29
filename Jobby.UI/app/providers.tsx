"use client";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import NavigationContext from "contexts/NavigationContext";
import { useCycle } from "framer-motion";

const Providers = ({
  children
}: {
  children: React.ReactNode;
}) => {
  const [isOpen, toggleOpen] = useCycle(false, true);
  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <NavigationContext.Provider value={{ isOpen, toggleOpen }}>
          {children}
      </NavigationContext.Provider>
    </QueryClientProvider>
  );
};

export default Providers;
