import { serverClient } from "../client";
import Link from "next/link";
import { useState } from "react";
import { PageContainer, ActionButton } from "../components/Common";
import {
  CreateBoardModal,
  DeleteBoardModal,
  EditBoardModal,
} from "../components/Modals/Dashboard";
import { GetServerSideProps, NextPage } from "next";
import { Board } from "../types";

export const getServerSideProps: GetServerSideProps = async ({ req }) => {
  const boards = await serverClient.get<Board[]>("/boards", req);

  return { props: { boards } };
};

export const Page: NextPage<{ boards: Board[] }> = ({ boards }) => {
  const [currentBoardList, setCurrentBoardList] = useState(boards);

  const [showCreateModal, setShowCreateModal] = useState<{
    visible: boolean;
  }>({
    visible: false,
  });

  const [showEditModal, setShowEditModal] = useState<{
    visible: boolean;
    board: Board | null;
  }>({
    visible: false,
    board: null,
  });

  const [showDeleteModal, setShowDeleteModal] = useState<{
    visible: boolean;
    boardId: string | null;
  }>({
    visible: false,
    boardId: null,
  });

  return (
    <PageContainer small title={"Dashboard"}>
      <CreateBoardModal
        setCurrentBoardList={setCurrentBoardList}
        setShowCreateModal={setShowCreateModal}
        showCreateModal={showCreateModal}
      />
      <DeleteBoardModal
        setCurrentBoardList={setCurrentBoardList}
        setShowDeleteModal={setShowDeleteModal}
        showDeleteModal={showDeleteModal}
      />
      <EditBoardModal
        setCurrentBoardList={setCurrentBoardList}
        setShowEditModal={setShowEditModal}
        showEditModal={showEditModal}
      />
      <ActionButton
        variant='primary'
        text='Create Board'
        rounded
        onClick={() => setShowCreateModal({ visible: true })}
      />
      <div className='grid w-full grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-3'>
        {currentBoardList.map((board) => (
          <div
            key={board.id}
            className='relative flex h-max w-full max-w-xs flex-col overflow-hidden border border-gray-300 bg-gray-50 transition-all delay-100'
          >
            <Link href={`/board/${board.id}`}>
              <a className='p-4'>
                <div className='flex h-28 flex-col gap-y-4'>
                  <p className='overflow-hidden whitespace-nowrap text-base font-medium'>
                    {board.name}
                  </p>
                  <p className='-mt-2 text-sm'>{board.createdDate}</p>
                </div>
              </a>
            </Link>
            <div className='absolute bottom-4 left-4 flex flex-row gap-x-4'>
              <ActionButton
                variant='secondary'
                text='Edit'
                onClick={() =>
                  setShowEditModal({
                    visible: true,
                    board: board,
                  })
                }
              />
              <ActionButton
                variant='danger'
                text='Remove'
                onClick={() =>
                  setShowDeleteModal({
                    visible: true,
                    boardId: board.id,
                  })
                }
              />
            </div>
          </div>
        ))}
      </div>
    </PageContainer>
  );
};

export default Page;
