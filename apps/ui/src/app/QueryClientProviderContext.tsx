"use client";

import { QueryClientProvider, QueryClient } from "@tanstack/react-query";

function QueryClientProviderContext({ children }: React.PropsWithChildren) {
  const client = new QueryClient();

  return <QueryClientProvider client={client}>{children}</QueryClientProvider>;
}

export default QueryClientProviderContext;
