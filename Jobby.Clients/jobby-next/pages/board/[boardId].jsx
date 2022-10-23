import Link from 'next/link';
import { getBoardById } from '/services/board/boardService';

export async function getServerSideProps({ query }) {
  const board = await getBoardById(query.boardId);

  return { props: { board } };
}

export default function Board({ board }) {
  return (
    <section className="p-8 flex flex-col gap-y-8 relative h-full">
      <div className="w-full max-w-7xl gap-8 grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5">
        {board.jobList.map((list) => (
          <div key={list.id} className="bg-gray-50 border-1 border-gray-900 flex flex-col h-max w-full">
            <div className="flex flex-col gap-y-2 p-4">
              <p className="text-base font-medium">{list.name}</p>
              <button className="text-white font-medium text-base bg-blue-500 hover:bg-gray-50 hover:text-black hover:border-main-blue border-1 py-2 px-8 w-max ml-auto !rounded-full">
                Add Job
              </button>
            </div>
            {list.jobs.map((job) => (
              <Link key={job.id} href={`/board/${board.id}/job/${job.id}`}>
                <a className="bg-gray-50 border-gray-900 border-t-1 w-full cursor-pointer p-4">
                  <div className="flex flex-col">
                    <p className="text-lg font-medium text-ellipsis overflow-hidden whitespace-nowrap">{job.title}</p>
                    <p className="text-base text-ellipsis overflow-hidden whitespace-nowrap">{job.company}</p>
                  </div>
                </a>
              </Link>
            ))}
          </div>
        ))}
        </div>
    </section>
  );
}
