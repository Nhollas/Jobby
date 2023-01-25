import Link from "next/link";
import { useState } from "react";
import { PageContainer } from "../../../../components/Common";
import Input from "../../../../components/Form/Input";
import { GetServerSideProps, NextPage } from "next";
import { serverClient } from "../../../../client";
import { Job } from "../../../../types";

export const getServerSideProps : GetServerSideProps = async ({ req, query }) => {
  const { boardId, jobId } = query;

  const job = await serverClient.get<Job>(`/board/${boardId}/job/${jobId}`, req)

  return { props: { job } };
}

const formatViewModel = (job : Job) => {
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
    appliedActivity,
    interviewActivities,
    offerActivities,
    acceptedOfferActivities,
  };
};

export const Page : NextPage<{ job: Job }> = ({ job }) => {
  const [model, setModel] = useState(formatViewModel(job));

  const {
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
      <section className='flex flex-col justify-center gap-y-8 border-x border-b border-gray-300 p-5'>
        <div className='activityFormToggle fixed inset-0'></div>
        <div className='flex flex-col gap-y-5'>
          <div className='flex flex-col gap-y-2'>
            <p className='text-sm font-medium'>Application</p>
            {appliedActivity === null ? (
              <button
                className='border-1 z-10 flex flex-row justify-start rounded-lg border-gray-300 bg-white px-5 py-1 text-sm'
              >
                <div className='flex w-full flex-row gap-x-4'>
                  <i className='bi bi-plus-square'></i>
                  <p className='text-sm'>Log Application</p>
                </div>
              </button>
            ) : (
              <form
                asp-page-handler='UpdateActivity'
                method='post'
                className='activityForm border-1 max-w-xxs z-10 h-full max-h-10 w-full cursor-pointer overflow-hidden border-gray-300 px-4'
              >
                <Input type='hidden' value={appliedActivity.id} name='id' />
                <Input type='hidden' value={appliedActivity.type} name='type' />
                <div className='flex h-10 flex-row items-center gap-x-2'>
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
                  className='min-h-[10rem] w-full border-0 pl-0'
                />
                <button className='btn-primary mb-3 rounded-sm px-5 py-0.5'>
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

export default Page;
