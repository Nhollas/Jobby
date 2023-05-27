"use client";

import NavigationContext from "contexts/NavigationContext";
import { usePathname } from "next/navigation";
import { useContext, useEffect, useState } from "react";
import { MenuItem } from "components/MenuItem";

type NavItem = {
  leaf: string;
  name: string;
  icon: JSX.Element;
};

const navItems: NavItem[] = [
  {
    leaf: "/",
    name: "Board",
    icon: <i className="bi bi-kanban text-xl text-slate-900"></i>,
  },
  {
    leaf: "activities",
    name: "Activities",
    icon: <i className="bi-list-ul text-xl text-slate-900"></i>,
  },
  {
    leaf: "contacts",
    name: "Contacts",
    icon: <i className="bi bi-people text-xl text-slate-900"></i>,
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
  const { toggleOpen } = useContext(NavigationContext);

  useEffect(() => {
    setSelected(leaf);
  }, [leaf]);

  return (
    <div className="relative flex w-full flex-row flex-wrap gap-4 border-b border-gray-300 p-4">
      {navItems.map(({ icon, leaf, name }) => (
        <MenuItem
          selected={selected === leaf}
          icon={icon}
          name={name}
          leaf={leaf}
          key={leaf}
          href={`/board/${pathnameSections[2]}/${leaf}`}
          layoutId={pathnameSections[2]}
          colour="bg-main-blue"
        />
      ))}
    </div>
  );
};
