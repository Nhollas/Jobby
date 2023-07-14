"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { MenuItem } from "@/components/MenuItem";
import { Layout, List, PanelLeft, Users } from "lucide-react";
import { SheetTrigger, Sheet } from "@/components/ui/sheet";
import { Button } from "@/components/ui/button";
import { GetBoardsResponse } from "@/contracts/queries/GetBoards";
import { MobileNavigationContent } from "./MobileNavigation";

type NavItem = {
  leaf: string;
  name: string;
  icon: JSX.Element;
};

const navItems: NavItem[] = [
  {
    leaf: "/",
    name: "Board",
    icon: <Layout className="h-4 w-4" />,
  },
  {
    leaf: "activities",
    name: "Activities",
    icon: <List className="h-4 w-4" />,
  },
  {
    leaf: "contacts",
    name: "Contacts",
    icon: <Users className="h-4 w-4" />,
  },
];

export const BoardNavigation = ({
  initialBoards,
}: {
  initialBoards: GetBoardsResponse;
}) => {
  const pathname = usePathname() || "/";
  const pathnameSections = pathname.split("/");
  const [open, setOpen] = useState(false);

  let leaf = pathnameSections[3] || "/";

  if (leaf === "job") {
    leaf = "/";
  }

  const [selected, setSelected] = useState(leaf);

  useEffect(() => {
    setSelected(leaf);
  }, [leaf]);

  return (
    <div className="z-10 flex h-16 w-full flex-row items-center gap-4 overflow-x-scroll border-b border-gray-200 px-4">
      <Sheet open={open} onOpenChange={setOpen}>
        <SheetTrigger asChild>
          <Button
            variant="outline"
            className="flex h-10 w-10 items-center gap-x-2 rounded-md px-2 py-1 text-sm md:hidden"
          >
            <PanelLeft className="h-5 w-5" />
            <span className="sr-only">Toggle Menu</span>
          </Button>
        </SheetTrigger>
        <MobileNavigationContent initialBoards={initialBoards} />
      </Sheet>
      {navItems.map(({ icon, leaf, name }) => (
        <MenuItem
          selected={selected === leaf}
          icon={icon}
          name={name}
          key={leaf}
          href={`/board/${pathnameSections[2]}/${
            leaf === "activities" ? `${leaf}/all` : leaf
          }`}
          layoutId={pathnameSections[2]}
        />
      ))}
    </div>
  );
};
