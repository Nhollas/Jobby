"use client";

import { ModalContext } from "contexts/ModalContext";
import { Board } from "../../types";
import Link from "next/link";
import { useContext, useState } from "react";
import { ActionButton } from "../Common";
import { CreateBoardModal } from "../Modals/CreateBoardModal";
import { DeleteBoardModal } from "../Modals/DeleteBoardModal";
import { AnimatePresence, motion } from "framer-motion";

type Props = {
  boards: Board[];
};

export function Boards({ boards }: Props) {
  const [currentBoardList, setCurrentBoardList] = useState(boards);

  const { handleModal } = useContext(ModalContext);

  return (
    <>
      <ActionButton
        variant='primary'
        text='Create Board'
        rounded
        onClick={() =>
          handleModal(
            <CreateBoardModal setCurrentBoardList={setCurrentBoardList} />
          )
        }
      />
      <div className='grid w-full grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3'>
        <AnimatePresence>
          {currentBoardList.map((board) => (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
              key={board.id}
              className='relative flex h-max w-full max-w-xs flex-col overflow-hidden border border-gray-300 bg-gray-50 transition-all delay-100'
            >
              <Link href={`/board/${board.id}`} className='w-full max-w-md p-4'>
                <div className='flex h-28 flex-col gap-y-4'>
                  <p className='overflow-hidden whitespace-nowrap text-base font-medium'>
                    {board.name}
                  </p>
                  <p className='-mt-2 text-sm'>
                    {" "}
                    {new Date(board.createdDate).toDateString()}
                  </p>
                </div>
              </Link>
              <div className='absolute bottom-4 left-4 flex flex-row gap-x-4'>
                <ActionButton
                  variant='danger'
                  text='Remove'
                  onClick={() =>
                    handleModal(
                      <DeleteBoardModal
                        boardId={board.id}
                        setCurrentBoardList={setCurrentBoardList}
                      />
                    )
                  }
                />
              </div>
            </motion.div>
          ))}
        </AnimatePresence>
      </div>
    </>
  );
}
