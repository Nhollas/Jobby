import Link from "next/link";
import { useState } from "react";
import { PageContainer } from "../../../../components/Common/PageContainer";
import Input from "../../../../components/Form/Input";
import { getJobById } from "/services/jobService";
import { getToken } from "next-auth/jwt";

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

  const job = await getJobById(query.boardId, query.jobId, token.accessToken);

  return { props: { jobProp: job } };
}

const formatViewModel = (job) => {
  const { activities } = job;
  const appliedActivity = activities.find((activity) => activity.type === 1);

  const interviewActivities = activities.filter(
    (activity) =>
      activity.type === 2 || activity.type === 3 || activity.type === 4
  );

  const offerActivities = activities.filter((activity) => activity.type === 5);

  const acceptedOfferActivities = activities.filter(
    (activity) => activity.type === 6
  );

  return {
    job,
    appliedActivity,
    interviewActivities,
    offerActivities,
    acceptedOfferActivities,
  };
};

export default function Job({ jobProp }) {
  const [model, setModel] = useState(formatViewModel(jobProp));

  const {
    job,
    appliedActivity,
    interviewActivities,
    offerActivities,
    acceptedOfferActivities,
  } = model;

  return (
    <PageContainer title>
      <Link href={`/board/${job.board.id}`}>
        <a className='relative flex w-max flex-row gap-4 border border-gray-300 px-8 py-2 text-base'>
          Back To Board
        </a>
      </Link>
      <div className='flex flex-row'>
        <div className='flex flex-col gap-y-2'>
          <h1 className='text-xl font-medium'>{job.title}</h1>
          <h2>{job.company}</h2>
        </div>
      </div>
      <section className='z-10 flex flex-row items-end gap-x-2'>
        <button className='relative border border-b-0 border-gray-300 px-6 py-1 text-sm font-medium'>
          Info
        </button>
        <button className='relative border border-b-0 border-gray-300 px-6 py-1 text-sm font-medium'>
          Activities
          {job.activities.length > 0 && (
            <span className='absolute top-1 right-2 flex h-4 w-4 items-center justify-center rounded-full border border-gray-300 text-xs'>
              {job.activities.length}
            </span>
          )}
        </button>
        <button className='relative border border-b-0 border-gray-300 px-6 py-1 text-sm font-medium'>
          Notes
          {job.notes.length > 0 && (
            <span className='absolute top-1 right-2 flex h-4 w-4 items-center justify-center rounded-full border border-gray-300 text-xs'>
              {job.notes.length}
            </span>
          )}
        </button>
        <button className='relative border border-b-0 border-gray-300 px-6 py-1 text-sm font-medium'>
          Contacts
          {job.contacts.length > 0 && (
            <span className='absolute top-1 right-2 flex h-4 w-4 items-center justify-center rounded-full border border-gray-300 text-xs'>
              {job.notes.length}
            </span>
          )}
        </button>
      </section>
      <section class='flex flex-col justify-center gap-y-8 border-x border-b border-gray-300 p-5'>
        <div class='activityFormToggle fixed inset-0'></div>
        <div class='flex flex-col gap-y-5'>
          <div class='flex flex-col gap-y-2'>
            <p class='text-sm font-medium'>Application</p>
            {appliedActivity === null ? (
              <button
                onclick='showElement("create-activity-parent")'
                class='border-1 z-10 flex flex-row justify-start rounded-lg border-gray-300 bg-white px-5 py-1 text-sm'
              >
                <div class='flex w-full flex-row gap-x-4'>
                  <i class='bi bi-plus-square'></i>
                  <p class='text-sm'>Log Application</p>
                </div>
              </button>
            ) : (
              <form
                asp-page-handler='UpdateActivity'
                method='post'
                class='activityForm border-1 max-w-xxs z-10 h-full max-h-10 w-full cursor-pointer overflow-hidden border-gray-300 px-4'
              >
                <Input type='hidden' value={appliedActivity.id} name='id' />
                <Input type='hidden' value={appliedActivity.type} name='type' />
                <div class='flex h-10 flex-row items-center gap-x-2'>
                  <Input
                    type='checkbox'
                    name='completed'
                    checked={appliedActivity.completed}
                  />
                  <Input
                    type='text'
                    value={appliedActivity.title}
                    name='title'
                  />
                </div>
                <Input
                  type='textarea'
                  name='note'
                  placeholder='Note'
                  value={appliedActivity.note}
                  class='min-h-[10rem] w-full border-0 pl-0'
                />
                <button class='btn-primary mb-3 rounded-sm px-5 py-0.5'>
                  Save
                </button>
              </form>
            )}
          </div>
        </div>
      </section>
    </PageContainer>
  );
}
