"use client";

import { useContext } from "react";
import NavigationContext from "contexts/NavigationContext";
import { motion, Variants } from "framer-motion";
import Link from "next/link";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";

export const BoardsBar = () => {
  const { isOpen } = useContext(NavigationContext);
  const { boards } = useContext(BoardsAndJobsContext);

  const list: Variants = {
    open: { opacity: 1, x: 16, transition: { ease: "linear" } },
    closed: { opacity: 0, x: -256, transition: { ease: "linear" } },
  };

  return (
    <>
      <motion.nav
        className="absolute bottom-4 top-[70px] left-0 z-20 flex w-64 flex-col gap-y-4 rounded-lg border border-gray-300 bg-white m-4 md:block"
        initial={false}
        animate={isOpen ? "open" : "closed"}
        variants={list}
      >
        <Link href="/dashboard" className="font-medium hover:underline">
          Boards
        </Link>
        <div className="flex flex-col gap-y-1">
          {boards.map((board) => (
            <Link
              key={board.id}
              href={`/board/${board.id}`}
              className="flex flex-row items-center gap-x-2 rounded-md py-1 text-sm text-gray-600 hover:bg-gray-100 truncate"
            >
              <i className="bi bi-kanban text-base text-slate-900"></i>
              {board.name}
            </Link>
          ))}
        </div>
      </motion.nav>
      <motion.nav className="hidden h-full w-full md:flex flex-col gap-y-4 border border-gray-300 p-4">
        <Link href="/dashboard" className="font-medium hover:underline ml-4">
          Boards
        </Link>
        <div className="flex flex-col gap-y-1">
          {boards.map((board) => (
            <Link
              key={board.id}
              href={`/board/${board.id}`}
              className="flex flex-row items-center gap-x-2 rounded-md py-1 text-sm text-gray-600 hover:bg-gray-100 px-4"
            >
              <i className="bi bi-kanban text-base text-slate-900"></i>
              <p className="truncate">{board.name}</p>
            </Link>
          ))}
        </div>
      </motion.nav>
    </>
  );
};