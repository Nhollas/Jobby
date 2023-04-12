"use client"

import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import Link from "next/link";
import { useContext } from "react";

export const BoardsBar = () => {
  const { boards } = useContext(BoardsAndJobsContext)

  return (
    <div className="flex flex-col w-full border-r border-b border-gray-300 p-4">
        <Link href="/dashboard" className="mb-2 font-medium px-4 hover:underline">Boards</Link>
        <div className="flex flex-col gap-y-1">
          {boards.map((board) => (
              <Link key={board.id} href={`/board/${board.id}`} className="text-sm text-gray-600 flex flex-row gap-x-2 items-center hover:bg-gray-100 py-1 px-4 rounded-md">
                <i className="bi bi-kanban text-base text-slate-900"></i>
                {board.name}
              </Link>
          ))}
          </div>
    </div>
  );
};
