import { ClerkProvider } from "@clerk/nextjs";
import QueryClientProviderContext from "./QueryClientProviderContext";

const Providers = ({ children }: { children: React.ReactNode }) => {
  return (
    <ClerkProvider>
      <QueryClientProviderContext>{children}</QueryClientProviderContext>
    </ClerkProvider>
  );
};

export default Providers;