"use client";

import { MainNavigation } from "@/components/MainNavigation";
import { Toaster } from "@/components/ui";

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <main className="grid min-h-screen grid-cols-1 md:grid-cols-[250px,1fr]">
      {/* <MobileNavigation initialBoards={initialBoards} /> */}
      <MainNavigation />
      {children}
      <Toaster />
    </main>
  );
}
