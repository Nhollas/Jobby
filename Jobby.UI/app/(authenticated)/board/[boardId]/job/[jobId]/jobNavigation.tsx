"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { Book, FileText, Info, List, Users } from "lucide-react";
import { Button } from "@/components/ui/button";
import Link from "next/link";
import { motion } from "framer-motion";
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";
import { cn } from "@/lib/utils";

type NavItem = {
  leaf: string;
  name: string;
  icon: JSX.Element;
};

const navItems: NavItem[] = [
  {
    leaf: "info",
    name: "Info",
    icon: <Info className="h-5 w-5" />,
  },
  {
    leaf: "activities",
    name: "Activities",
    icon: <List className="h-5 w-5" />,
  },
  {
    leaf: "notes",
    name: "Notes",
    icon: <Book className="h-5 w-5" />,
  },
  {
    leaf: "contacts",
    name: "Contacts",
    icon: <Users className="h-5 w-5" />,
  },
  {
    leaf: "documents",
    name: "Documents",
    icon: <FileText className="h-5 w-5" />,
  },
];

export const JobNavigation = ({
  jobId,
  boardId,
}: {
  jobId: string;
  boardId: string;
}) => {
  const pathname = usePathname() || "/";
  const pathnameSections = pathname.split("/");

  const [selected, setSelected] = useState(pathnameSections[5]);

  useEffect(() => {
    setSelected(pathnameSections[5]);
  }, [pathnameSections]);

  return (
    <ScrollArea className="w-screen rounded-md px-4 pt-4 sm:w-max">
      <div className="flex items-center justify-center gap-x-2">
        {navItems.map(({ icon, leaf, name }) => (
          <Button key={leaf} asChild variant="outline">
            <Link
              href={`/board/${boardId}/job/${jobId}/${leaf}`}
              className={cn(
                "flex items-center gap-x-2 rounded-md px-4 py-2 text-sm",
                selected === leaf
                  ? "bg-white font-medium text-primary"
                  : "font-medium text-muted-foreground"
              )}
            >
              {icon}
              {name}
            </Link>
          </Button>
        ))}
      </div>
      <ScrollBar orientation="horizontal" className="invisible" />
    </ScrollArea>
  );
};
