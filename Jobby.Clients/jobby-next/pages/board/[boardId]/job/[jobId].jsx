import Link from 'next/link';
import { getJobById } from '/services/job/jobService';

export async function getServerSideProps({ query }) {
  const job = await getJobById(query.boardId, query.jobId);

  console.log(job);

  return { props: { job } };
}

export default function Job({ job }) {
  return (
    <section className="flex flex-col gap-y-6 w-full max-w-5xl justify-center relative bg-white rounded-xl p-5">
      <Link href={`/board/${job.board.id}`}>
        <a className="font-medium text-gray-900 py-2 px-8 w-max text-center z-10">Back To Board</a>
      </Link>
      <div className="flex flex-row">
          <div className="flex flex-col gap-y-2">
              <p className="text-xl font-medium rounded-lg w-max">Job</p>
              <h1 className="text-xl font-medium">{job.title}</h1>
              <h2>{job.company}</h2> 
          </div>
      </div>
      <section className="flex flex-row z-10 items-end gap-x-2">
        <h2>Tabs</h2>
        <button>Info</button>
        <button>
            Activities
            {job.activities.length > 0 &&
              <span className="border-1 border-gray-300 w-4 h-4 rounded-full flex justify-center items-center absolute top-1 right-2 text-xs">{job.activities.count}</span>
            }
        </button>
        <button>
            Notes
            {job.notes.length > 0 &&
              <span className="border-1 border-gray-300 w-4 h-4 rounded-full flex justify-center items-center absolute top-1 right-2 text-xs">{job.notes.count}</span>
            }
        </button>
        <button>
            Contacts
            {job.contacts.length > 0 &&
              <span className="border-1 border-gray-300 w-4 h-4 rounded-full flex justify-center items-center absolute top-1 right-2 text-xs">{job.notes.count}</span>
            }
        </button>
      </section>
    </section>
  )
}
