import { serverApi } from "@/lib/clients";
import { Toaster } from "@/components/ui/toaster";
import { Button } from "@/components/ui/button";
import Link from "next/link";
import { Separator } from "@/components/ui/separator";
import { Users } from "lucide-react";
import { BoardsBar } from "./BoardsBar";
import { getBoards } from "@/contracts/queries/GetBoards";

export default async function Layout({
  children,
}: {
  children: React.ReactNode;
}) {
  const initialBoards = await getBoards(serverApi);

  return (
    <main className="grid min-h-screen grid-cols-[250px,1fr]">
      <div className="relative z-50">
        <div className="fixed top-0 left-0 h-screen w-[250px] border-r border-gray-200 py-6">
          <div className="flex w-full flex-col gap-y-4">
            <div className="py-2 pt-0">
              <h2 className="mb-2 px-6 text-lg font-semibold tracking-tight">
                Track
              </h2>
              <div className="space-y-1 px-2">
                <Button
                  asChild
                  variant="ghost"
                  className="w-full justify-start font-normal"
                >
                  <Link href={`/contacts`}>
                    <Users className="mr-2 h-4 w-4" />
                    Contacts
                  </Link>
                </Button>
              </div>
            </div>
            <Separator />
            <div className="m-0 py-2">
              <Button
                variant="link"
                asChild
                className="px-6 text-lg font-semibold tracking-tight"
              >
                <Link href="/boards">Boards</Link>
              </Button>
              <BoardsBar initialBoards={initialBoards} />
            </div>
            <Separator />
          </div>
        </div>
      </div>
      {children}
      <Toaster />
    </main>
  );
}
