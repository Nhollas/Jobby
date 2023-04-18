"use client";

import Link from "next/link";
import { useContext } from "react";
import { AnimatePresence, motion } from "framer-motion";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";

export function Boards() {
  const { boards } = useContext(BoardsAndJobsContext);

  return (
    <div className="flex flex-col gap-y-4 p-4 lg:px-8">
      <Link
        href="/create-board"
        className="w-max rounded-full border bg-main-blue py-2 px-8 text-base font-medium text-white hover:border-main-blue hover:bg-gray-50 hover:text-black"
      >
        Create Board
      </Link>
      <div className="grid grid-cols-[repeat(auto-fill,minmax(250px,1fr))] gap-8">
        <AnimatePresence>
          {boards.map((board) => (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
              key={board.id}
              className="relative flex h-max w-full max-w-xs flex-col overflow-hidden border border-gray-300 bg-gray-50 transition-all delay-100"
            >
              <Link href={`/board/${board.id}`} className="w-full max-w-md p-4">
                <div className="flex h-28 flex-col gap-y-4">
                  <p className="overflow-hidden whitespace-nowrap text-base font-medium">
                    {board.name}
                  </p>
                  <p className="-mt-2 text-sm">
                    {" "}
                    {new Date(board.createdDate).toDateString()}
                  </p>
                </div>
              </Link>
              <div className="absolute bottom-4 left-4 flex flex-row gap-x-4">
                <Link
                  href={`/delete-board/${board.id}`}
                  className="rounded-lg bg-main-red px-4 py-2 font-medium text-white"
                >
                  Delete
                </Link>
              </div>
            </motion.div>
          ))}
        </AnimatePresence>
      </div>
    </div>
  );
}
