"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { MenuItem } from "components/MenuItem";
import { Layout, List, Users } from "lucide-react";

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

export const BoardNavigation = () => {
  const pathname = usePathname() || "/";
  const pathnameSections = pathname.split("/");

  let leaf = pathnameSections[3] || "/";

  if (leaf === "job") {
    leaf = "/";
  }

  const [selected, setSelected] = useState(leaf);

  useEffect(() => {
    setSelected(leaf);
  }, [leaf]);

  return (
    <div className="fixed top-0 z-10 flex h-16 w-full flex-row flex-wrap items-center gap-4 border-b border-gray-300 bg-white px-4">
      {navItems.map(({ icon, leaf, name }) => (
        <MenuItem
          selected={selected === leaf}
          icon={icon}
          name={name}
          key={leaf}
          href={`/board/${pathnameSections[2]}/${leaf}`}
          layoutId={pathnameSections[2]}
        />
      ))}
    </div>
  );
};
