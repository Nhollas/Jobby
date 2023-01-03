import Link from "next/link";
import { useState } from "react";
import { PageContainer } from "../../components/Common/PageContainer";
import { CreateJobModal } from "../../components/Modals/Job/CreateJobModal";
import { getBoardById } from "/services/boardService";
import { getToken } from "next-auth/jwt";
import { MultipleContainers } from "../../components";

export async function getServerSideProps({ query, req }) {
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

  const board = await getBoardById(query.boardId, accessToken);

  return { props: { board, accessToken } };
}

export default function Page({ board, accessToken }) {
  const [currentBoard, setCurrentBoard] = useState(board);

  const { jobList, name, activitiesCount, contactsCount } = currentBoard;

  const [showCreateModal, setShowCreateModal] = useState({
    visible: false,
    board: null,
    jobList: null,
  });

  return (
    <PageContainer extended>
      <CreateJobModal
        setCurrentBoard={setCurrentBoard}
        setShowCreateModal={setShowCreateModal}
        showCreateModal={showCreateModal}
        accessToken={accessToken}
      />
      <div className='flex w-full flex-col gap-y-4'>
        <h1 className='text-2xl font-medium'>{name}</h1>
        <div className='flex flex-row gap-x-4'>
          <Link href={`/board/${board.id}/activities`}>
            <a className='relative flex flex-row gap-4 border border-gray-300 bg-white px-8 py-2 text-base'>
              Activities
              {activitiesCount > 0 && (
                <div className='absolute top-2/4 right-2 flex h-5 w-5 translate-y-[-50%] items-center justify-center rounded-full border border-gray-300 '>
                  <p className='text-sm font-medium'>{activitiesCount}</p>
                </div>
              )}
            </a>
          </Link>
          <Link href={`/board/${board.id}/contacts`}>
            <a className='relative flex flex-row gap-4 border border-gray-300 bg-white px-8 py-2 text-base'>
              Contacts
              {contactsCount > 0 && (
                <div className='absolute top-1 right-1 flex h-5 w-5 items-center justify-center rounded-full border border-gray-300'>
                  <p className='text-sm'>{contactsCount}</p>
                </div>
              )}
            </a>
          </Link>
          <button className='ml-auto rounded-full border border-gray-300 bg-white px-8 py-2 font-medium'>
            Actions
          </button>
        </div>
      </div>
      <MultipleContainers lists={jobList} />
    </PageContainer>
  );
}
