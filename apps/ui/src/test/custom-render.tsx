import { QueryClientProvider } from "@tanstack/react-query";
import { render } from "./test-utils";
import { queryClient } from "@/lib/react-query";
import { Toaster } from "@/components/ui";

export function renderWithProviders(children: React.ReactNode) {
  return render(
    <QueryClientProvider client={queryClient}>
      {children}
      <Toaster />
    </QueryClientProvider>
  );
}
