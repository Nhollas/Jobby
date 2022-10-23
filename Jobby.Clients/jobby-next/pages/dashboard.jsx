import Link from 'next/link';
import { boardList } from '/services/board/boardService'

export async function getServerSideProps(context) {

  var boards = await boardList();

  return { props: { boards }}
}

export default function Dashboard ({ boards }) {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-5 w-full max-w-5xl">
      {boards.map((board) => (
        <Link key={board.id} href={`/board/${board.id}`}>
          <a className="w-full sm:max-w-none border-1 border-gray-900 bg-gray-50">
            <div className="flex flex-col gap-y-4 p-4">
                <p className="text-base font-medium overflow-hidden whitespace-nowrap">{board.name}</p>
                <p className="text-sm -mt-2">{board.createdDate}</p>
            </div>
          </a>
        </Link>
      ))}
    </div>
  )
}
  