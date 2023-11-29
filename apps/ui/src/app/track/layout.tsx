"use client";
import { Toaster } from "@/components/ui";
import { BoardSideNavigation, MobileBoardNavigation } from "@/features/board";

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <main className="grid min-h-screen grid-cols-1 md:grid-cols-[250px,1fr]">
      <MobileBoardNavigation />
      <BoardSideNavigation />
      {children}
      <Toaster />
    </main>
  );
}
