"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { NavigationItem } from "@/components";
import { Layout, List, Users } from "lucide-react";
import { MobileBoardNavigation } from "./MobileBoardNavigation";

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

export function BoardTopNavigation() {
  const pathname = usePathname() || "/";
  const pathnameSections = pathname.split("/");

  let leaf = pathnameSections[4] || "/";

  if (leaf === "job") {
    leaf = "/";
  }

  const [selected, setSelected] = useState(leaf);

  useEffect(() => {
    setSelected(leaf);
  }, [leaf]);

  return (
    <div className="z-10 flex h-16 w-full flex-row items-center gap-4 overflow-x-auto border-b border-gray-200 px-4">
      <MobileBoardNavigation />
      {navItems.map(({ icon, leaf, name }) => (
        <NavigationItem
          selected={selected === leaf}
          icon={icon}
          name={name}
          key={leaf}
          href={`/track/board/${pathnameSections[3]}/${
            leaf === "activities" ? `${leaf}/all` : leaf
          }`}
          layoutId={pathnameSections[2]}
        />
      ))}
    </div>
  );
}
