"use client";

import Link from "next/link";
import { Button } from "./ui/button";
import { PanelLeft, Users } from "lucide-react";
import { Separator } from "./ui/separator";
import { BoardsBar } from "@/components/BoardsBar";
import { SignOut } from "@/components/SignOut";
import { GetBoardsResponse } from "@/contracts/queries/GetBoards";
import { useState } from "react";
import { Sheet, SheetContent, SheetTrigger } from "./ui/sheet";
import { usePathname } from "next/navigation";

export function MobileNavigation({
  initialBoards,
}: {
  initialBoards: GetBoardsResponse;
}) {
  const [open, setOpen] = useState(false);

  const pathname = usePathname() || "/";
  const isBoardRoute = pathname.includes("/board");
  const isBoardsRoute = pathname.includes("/boards");

  return (
    !isBoardRoute ||
    (isBoardsRoute && (
      <Sheet open={open} onOpenChange={setOpen}>
        <SheetTrigger asChild>
          <Button
            variant="outline"
            className="absolute top-6 right-6 z-10 h-10 w-10 bg-white p-0 text-base focus-visible:ring-0 focus-visible:ring-offset-0 md:hidden"
          >
            <PanelLeft className="h-5 w-5" />
            <span className="sr-only">Toggle Menu</span>
          </Button>
        </SheetTrigger>
        <MobileNavigationContent initialBoards={initialBoards} />
      </Sheet>
    ))
  );
}

export function MobileNavigationContent({
  initialBoards,
}: {
  initialBoards: GetBoardsResponse;
}) {
  return (
    <SheetContent side="left" className="p-0 py-6">
      <div className="flex w-full flex-col gap-y-4">
        <div>
          <h2 className="px-8 text-lg font-semibold tracking-tight">Track</h2>
          <div className="space-y-1 px-4">
            <Button
              asChild
              variant="ghost"
              className="w-full justify-start font-normal"
            >
              <Link href="/contacts">
                <Users className="mr-2 h-4 w-4" />
                Contacts
              </Link>
            </Button>
          </div>
        </div>
        <Separator />
        <div className="">
          <Button
            variant="link"
            asChild
            className="h-max px-8 py-0 text-lg font-semibold tracking-tight"
          >
            <Link href="/boards">Boards</Link>
          </Button>
          <BoardsBar initialBoards={initialBoards} />
        </div>
        <Separator />
        <SignOut />
      </div>
    </SheetContent>
  );
}
