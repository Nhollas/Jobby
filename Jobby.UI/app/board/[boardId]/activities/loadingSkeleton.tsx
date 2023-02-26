"use client";

export const ActivitiesSkeleton = () => {
  const activities = [
    {
      id: 1,
    },
    {
      id: 2,
    },
    {
      id: 3,
    },
    {
      id: 4,
    },
    {
      id: 5,
    },
    {
      id: 6,
    },
  ];

  return (
    <section className='flex flex-col gap-y-4'>
      {activities.map((activity) => (
        <div
          className='grid h-[56px] w-full grid-cols-[minmax(100px,1fr)_1fr_1fr] items-center gap-x-4 border border-gray-300 bg-gray-50 p-4 md:grid-cols-[minmax(100px,175px)_1fr_1fr]'
          key={activity.id}
        >
          <div className='h-6 w-20 animate-pulse rounded-md bg-gray-200'></div>
          <div className='h-6 w-[6.5rem] animate-pulse rounded-md bg-gray-200'></div>
          <div className='h-6 w-44 animate-pulse justify-self-end rounded-md bg-gray-200'></div>
        </div>
      ))}
    </section>
  );
};
