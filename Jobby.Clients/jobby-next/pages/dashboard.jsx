import { getToken } from "next-auth/jwt";
import Link from "next/link";
import { useState } from "react";
import { PageContainer } from "../components/Common/PageContainer";
import { CreateBoardModal } from "../components/Modals/Dashboard/CreateBoardModal";
import { DeleteBoardModal } from "../components/Modals/Dashboard/DeleteBoardModal";
import { EditBoardModal } from "../components/Modals/Dashboard/EditBoardModal";
import ActionButton from "../components/Common/ActionButton";
import { boardList } from "/services/boardService";

export async function getServerSideProps({ req }) {
  const token = await getToken({ req });

  if (!token) {
    return {
      redirect: {
        destination: "/login",
        permanent: false,
      },
    };
  }

  const { accessToken } = token;

  var boards = await boardList(token.accessToken);

  return { props: { boards, accessToken } };
}

export default function Dashboard({ boards, accessToken }) {
  const [currentBoardList, setCurrentBoardList] = useState(boards);

  const [showCreateModal, setShowCreateModal] = useState({
    visible: false,
  });

  const [showEditModal, setShowEditModal] = useState({
    visible: false,
    board: null,
  });

  const [showDeleteModal, setShowDeleteModal] = useState({
    visible: false,
    boardId: null,
  });

  return (
    <PageContainer small title={"Dashboard"}>
      <CreateBoardModal
        setCurrentBoardList={setCurrentBoardList}
        setShowCreateModal={setShowCreateModal}
        visible={showCreateModal.visible}
        accessToken={accessToken}
      />
      <DeleteBoardModal
        setCurrentBoardList={setCurrentBoardList}
        setShowDeleteModal={setShowDeleteModal}
        boardId={showDeleteModal.boardId}
        visible={showDeleteModal.visible}
        accessToken={accessToken}
      />
      <EditBoardModal
        setCurrentBoardList={setCurrentBoardList}
        setShowEditModal={setShowEditModal}
        board={showEditModal.board}
        visible={showEditModal.visible}
        accessToken={accessToken}
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
}
