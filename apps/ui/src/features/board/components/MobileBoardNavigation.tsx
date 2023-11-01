"use client";

import Link from "next/link";
import { PanelLeft, Users } from "lucide-react";
import { useState } from "react";
import {
  Sheet,
  SheetContent,
  SheetTrigger,
  Button,
  Separator,
  SignOut,
} from "@/components/ui";
import { usePathname } from "next/navigation";
import { BoardsBar } from "./BoardsBar";

export function MobileBoardNavigation() {
  const [open, setOpen] = useState(false);
  const pathname = usePathname() || "/";
  const isBoardRoute = pathname.includes("/board");
  const isBoardsRoute = pathname === "/track/boards";
  const isContactsRoute = pathname === "/track/contacts";
  const isContactRoute = pathname.includes("/track/contact");

  const requiredNavRoute =
    (isBoardRoute && isBoardsRoute) || isContactRoute || isContactsRoute;

  return (
    requiredNavRoute && (
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
        <MobileNavigationContent />
      </Sheet>
    )
  );
}

export function MobileNavigationContent() {
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
              <Link href="/track/contacts">
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
            <Link href="/track/boards">Boards</Link>
          </Button>
          <BoardsBar />
        </div>
        <Separator />
        <SignOut />
      </div>
    </SheetContent>
  );
}
