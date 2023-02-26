"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { MenuItem } from "../../../../../components/MenuItem";

type NavItem = {
  leaf: string;
  name: string;
  icon: JSX.Element;
};

const navItems: NavItem[] = [
  {
    leaf: "info",
    name: "Info",
    icon: <i className='bi bi-info-circle text-xl text-slate-900'></i>,
  },
  {
    leaf: "activities",
    name: "Activities",
    icon: <i className='bi-list-ul text-xl text-slate-900'></i>,
  },
  {
    leaf: "notes",
    name: "Notes",
    icon: <i className='bi bi-journal text-xl text-slate-900'></i>,
  },
  {
    leaf: "contacts",
    name: "Contacts",
    icon: <i className='bi bi-people text-xl text-slate-900'></i>,
  },
  {
    leaf: "documents",
    name: "Documents",
    icon: <i className='bi bi-file-earmark-text text-xl text-slate-900'></i>,
  },
];

export const JobNavigation = ({ jobId, boardId }) => {
  const pathname = usePathname() || "/";
  const pathnameSections = pathname.split("/");

  const [selected, setSelected] = useState(pathnameSections[5]);

  useEffect(() => {
    setSelected(pathnameSections[5]);
  }, [pathnameSections]);

  return (
    <div className='relative flex flex-col gap-y-2'>
      <div className='flex flex-row flex-wrap gap-4'>
        {navItems.map(({ icon, leaf, name }) => (
          <MenuItem
            selected={selected === leaf}
            icon={icon}
            name={name}
            leaf={leaf}
            key={leaf}
            href={`/board/${boardId}/job/${jobId}/${leaf}`}
            layoutId={jobId}
            colour='bg-main-red'
          />
        ))}
      </div>
    </div>
  );
};
