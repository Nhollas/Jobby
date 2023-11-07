import { ClerkProvider } from "@clerk/nextjs";
import ClientQueryClientProvider from "./ClientQueryClientProvider";

export const Providers = ({ children }: { children: React.ReactNode }) => {
  return (
    <ClerkProvider>
      <ClientQueryClientProvider>{children}</ClientQueryClientProvider>
    </ClerkProvider>
  );
};
