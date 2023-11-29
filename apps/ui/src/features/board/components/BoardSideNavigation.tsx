"use client";

import Link from "next/link";
import { Users } from "lucide-react";
import { SignOut, Separator, Button } from "@/components/ui";
import { BoardsBar } from "@/features/board";

export function BoardSideNavigation() {
  return (
    <div className="relative z-10 hidden md:block">
      <div className="fixed top-0 left-0 h-screen w-[250px] border-r border-gray-200 py-6">
        <div className="flex w-full flex-col gap-y-4">
          <div className="space-y-2">
            <h2 className="px-8 text-lg font-semibold tracking-tight">Track</h2>
            <Button
              asChild
              variant="ghost"
              className="mx-4 w-[calc(100%-2rem)] justify-start font-normal"
            >
              <Link href="/track/contacts">
                <Users className="mr-2 h-4 w-4" />
                Contacts
              </Link>
            </Button>
          </div>
          <Separator />
          <div className="space-y-2">
            <Button
              variant="link"
              asChild
              className="h-max px-8 py-0 text-lg font-semibold tracking-tight"
            >
              <Link href="/track/boards">Boards</Link>
            </Button>
            <BoardsBar />
          </div>
          <Separator />
          <SignOut />
        </div>
      </div>
    </div>
  );
}
