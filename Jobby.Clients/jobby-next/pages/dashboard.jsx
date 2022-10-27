import Link from 'next/link';
import { useState } from 'react';
import { CreateBoardModal } from '../components/Modals/Dashboard/CreateBoardModal';
import { DeleteBoardModal } from '../components/Modals/Dashboard/DeleteBoardModal';
import { EditBoardModal } from '../components/Modals/Dashboard/EditBoardModal';
import { boardList } from '/services/board/boardService'

export async function getServerSideProps() {

  var boards = await boardList();

  return { props: { boards }}
}

export default function Dashboard ({ boards }) {
  const [currentBoardList, setCurrentBoardList] = useState(boards)

  const [showCreateModal, setShowCreateModal] = useState({
    visible: false
  })

  const [showEditModal, setShowEditModal] = useState({
    visible: false,
    board: null
  })

  const [showDeleteModal, setShowDeleteModal] = useState({
    visible: false,
    board: null
  })

  return (
    <div className='flex flex-col relative bg-white/90 p-8 w-full max-w-5xl gap-y-8'>
      <button 
        onClick={() => setShowCreateModal({ visible: true })}
        className="text-white font-raleway font-medium text-base bg-main-blue hover:bg-gray-50 hover:text-black hover:border-main-blue border-1 py-2 px-8 w-max !rounded-full">
        Create Board
      </button>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-5 w-full">
        {currentBoardList.map((board) => (
          <div 
            key={board.id}
            className='w-full sm:max-w-none p-4 border-1 border-gray-300 flex flex-col bg-gray-50 h-max overflow-hidden delay-100 transition-all relative'>
            <Link href={`/board/${board.id}`}>
              <a>
                <div className="flex flex-col gap-y-4 h-24">
                    <p className="text-base font-medium overflow-hidden whitespace-nowrap">{board.name}</p>
                    <p className="text-sm -mt-2">{board.createdDate}</p>
                </div>
              </a>
            </Link>
            <div className='absolute bottom-4 flex flex-row gap-x-4'>
              <button
                onClick={() => setShowEditModal({ 
                  visible: true, 
                  board: board
                })} 
                className='px-5 py-1.5 text-sm font-medium outline outline-1 outline-gray-300 bg-white'>
                Edit
              </button>
              <button 
                onClick={() => setShowDeleteModal({ 
                  visible: true, 
                  board: board
                })} 
                className='text-sm text-white font-medium bg-main-red px-5 py-1.5 rounded-lg'>
                Remove
              </button>
            </div>
          </div>
          )
        )}

        <CreateBoardModal
          boardListState={setCurrentBoardList}
          parentState={setShowCreateModal}
          boardList={currentBoardList}
          visible={showCreateModal.visible}
        />

        <DeleteBoardModal
          boardListState={setCurrentBoardList}
          parentState={setShowDeleteModal}
          boardList={currentBoardList}
          board={showDeleteModal.board}
          visible={showDeleteModal.visible}
        />

        <EditBoardModal
          boardListState={setCurrentBoardList}
          parentState={setShowEditModal}
          boardList={currentBoardList}
          board={showEditModal.board}
          visible={showEditModal.visible}
        />
      </div>
    </div>
  )
}
  