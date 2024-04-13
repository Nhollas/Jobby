"use client";
import { Toaster } from "@/components/ui";
import { BoardSideNavigation } from "@/features/board";

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <main className="grid min-h-screen grid-cols-1 xl:grid-cols-[250px,1fr]">
      <BoardSideNavigation />
      {children}
      <Toaster />
    </main>
  );
}
