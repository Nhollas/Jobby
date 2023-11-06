"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { Book, FileText, Info, List, Users } from "lucide-react";
import { ScrollArea, ScrollBar } from "@/components/ui";
import { NavigationItem } from "@/components";

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
  jobRef,
  boardRef,
}: {
  jobRef: string;
  boardRef: string;
}) => {
  const pathname = usePathname() || "/";
  const pathnameSections = pathname.split("/");

  const [selected, setSelected] = useState(pathnameSections[5]);

  useEffect(() => {
    setSelected(pathnameSections[5]);
  }, [pathnameSections]);

  return (
    <div className="border-b border-gray-200 p-4">
      <ScrollArea className="w-screen rounded-md sm:w-max">
        <div className="flex items-center justify-center gap-x-2">
          {navItems.map(({ icon, leaf, name }) => (
            <NavigationItem
              selected={selected === leaf}
              icon={icon}
              name={name}
              key={leaf}
              href={`/track/board/${boardRef}/job/${jobRef}/${
                leaf === "activities" ? `${leaf}/all` : leaf
              }`}
              layoutId={jobRef}
            />
          ))}
        </div>
        <ScrollBar orientation="horizontal" className="invisible" />
      </ScrollArea>
    </div>
  );
};
