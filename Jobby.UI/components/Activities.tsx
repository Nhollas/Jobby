"use client";

import { Activity } from "types";
import { ActionButton } from "components/Common";
import { Reducer, useReducer } from "react";
import reducer from "reducers/ActivityListReducer";
import { client } from "clients";
import Input from "components/Common/Input";

type Props = {
  activities: Activity[];
};

const activityTypeToCssClass = (type: number) => {
  const activityTypeCssClasses: Record<number, string> = {
    1: "bg-red-50 text-red-500",
    2: "bg-orange-50 text-orange-500",
    3: "bg-amber-50 text-amber-500",
    4: "bg-yellow-50 text-yellow-500",
    5: "bg-lime-50 text-lime-500",
    6: "bg-green-50 text-green-500",
    7: "bg-emerald-50 text-emerald-500",
    8: "bg-teal-50 text-teal-500",
    9: "bg-cyan-50 text-cyan-500",
    10: "bg-sky-50 text-sky-500",
    11: "bg-blue-50 text-blue-500",
    12: "bg-indigo-50 text-indigo-500",
    13: "bg-violet-50 text-violet-500",
    14: "bg-purple-50 text-purple-500",
    15: "bg-fuchsia-50 text-fuchsia-500",
    16: "bg-pink-50 text-pink-500",
    17: "bg-rose-50 text-rose-500",
    18: "bg-red-50 text-red-500",
    19: "bg-orange-50 text-orange-500",
    20: "bg-amber-50 text-amber-500",
    21: "bg-yellow-50 text-yellow-500",
    22: "bg-lime-50 text-lime-500",
    23: "bg-green-50 text-green-500",
  };

  return activityTypeCssClasses[type];
};

export const Activities = ({ activities }: Props) => {
  const [state, dispatch] = useReducer<
    Reducer<{ activities: Activity[] }, any>
  >(reducer, { activities });

  const handleChange = (event: any, activityId: string) => {
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: event.target.name,
      activityId,
      value:
        event.target.type === "checkbox"
          ? event.target.checked
          : event.target.value,
    });
  };

  const showActivity = (e: any) => {
    e.target.classList.remove("h-14");
    e.target.classList.add("h-full");
  };

  const handleSubmit = async (event: any, activityId: string) => {
    event.preventDefault();

    //TODO: Validation with YUP package.

    const activity = state.activities.find(
      (activity) => activity.id === activityId
    );

    if (activity) {
      await client.put<Activity, any>("/activity/update", activity);
    }
  };

  return state.activities.length === 0 ? (
    <h1>No Activities Found.</h1>
  ) : (
    <section className='grid grid-cols-1 gap-4'>
      {state.activities.map((activity: Activity) => (
        <div
          className='transition-height relative flex h-14 w-full cursor-pointer flex-col gap-y-5 overflow-y-hidden  border border-gray-300 bg-gray-50 p-4 duration-200 ease-out'
          onClick={showActivity}
          key={activity.id}
        >
          <form
            onSubmit={(e) => handleSubmit(e, activity.id)}
            className='flex flex-col gap-y-5'
          >
            <Input type='hidden' value={activity.id} name='id' />
            <Input type='hidden' value={activity.type} name='type' />
            <div className='grid w-full grid-cols-[minmax(100px,1fr)_1fr_1fr] items-center gap-x-4 md:grid-cols-[minmax(100px,175px)_1fr_1fr]'>
              <div className='flex w-full flex-row items-center gap-x-4'>
                <Input
                  type='checkbox'
                  name='completed'
                  checked={activity.completed}
                  onChange={(e) => handleChange(e, activity.id)}
                />
                <Input
                  className='m-0 w-full cursor-pointer border-0 bg-gray-50 text-sm'
                  type='text'
                  value={activity.title}
                  name='title'
                  onChange={(e) => handleChange(e, activity.id)}
                />
              </div>
              <div className='flex flex-col gap-2 overflow-hidden md:flex-row'>
                <p className='inline-block w-max truncate rounded-md bg-blue-50 px-3 py-1 text-xs font-medium text-blue-600'>
                  {activity.job.title}
                </p>
                <p className='inline-block w-max truncate rounded-md bg-blue-50 px-3 py-1 text-xs font-medium text-blue-600'>
                  {activity.job.company}
                </p>
              </div>
              <div className='flex flex-col gap-2 justify-self-end md:flex-row'>
                <p
                  className={`${activityTypeToCssClass(
                    activity.type
                  )} ml-auto flex w-max items-center truncate rounded-md px-3 py-0.5 text-xs font-medium`}
                >
                  {activity.name}
                </p>
                <p className='ml-auto flex w-max justify-center truncate rounded-md bg-gray-100 px-3 py-1 text-xs font-medium text-gray-600'>
                  {new Date(activity.createdDate).toDateString()}
                </p>
              </div>
            </div>
            <Input
              className='min-h-[8rem] w-full border border-gray-300 p-3'
              name='note'
              type='textarea'
              value={activity.note}
              placeholder='Note'
              onChange={(e) => handleChange(e, activity.id)}
            />
            <ActionButton variant='primary' text='Save' />
          </form>
        </div>
      ))}
    </section>
  )}
