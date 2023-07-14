import { serverApi } from "@/lib/clients";
import { Toaster } from "@/components/ui/toaster";
import { getBoards } from "@/contracts/queries/GetBoards";
import { MobileNavigation } from "@/components/MobileNavigation";
import { MainNavigation } from "@/components/MainNavigation";

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  const initialBoards = await getBoards(serverApi);

  return (
    <main className="grid min-h-screen grid-cols-1 md:grid-cols-[250px,1fr]">
      <MobileNavigation initialBoards={initialBoards} />
      <MainNavigation initialBoards={initialBoards} />
      {children}
      <Toaster />
    </main>
  );
}
