import Link from "next/link";
import { serverClient } from "../../client";
import { useState } from "react";
import { PageContainer } from "../../components/Common";
import { MultipleContainers } from "../../components";
import { Board, JobList } from "../../types";
import { GetServerSideProps, NextPage } from "next";

export const getServerSideProps: GetServerSideProps = async ({
  query,
  req,
}) => {
  const board = await serverClient.get<Board>(`/board/${query.boardId}`, req);

  return { props: { board } };
};

export const Page: NextPage<{ board: Board }> = ({ board }) => {
  const [currentBoard, setCurrentBoard] = useState(board);

  const { jobList, name, activitiesCount, contactsCount } = currentBoard;

  const [showCreateModal, setShowCreateModal] = useState<{
    visible: boolean;
    board: Board | null;
    jobList: JobList[] | null;
  }>({
    visible: false,
    board: null,
    jobList: null,
  });

  return (
    <PageContainer extended>
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
};

export default Page;
