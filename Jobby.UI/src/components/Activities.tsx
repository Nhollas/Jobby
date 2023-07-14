"use client";

import { Activity } from "@/types";
import Link from "next/link";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { Briefcase, Edit } from "lucide-react";
import { Badge } from "@/components/ui/badge";
import { AnimatePresence, motion, Variants } from "framer-motion";
import { useEffect, useState } from "react";
import { Textarea } from "@/components/ui/textarea";
import { UpdateActivityDetailsRequest } from "@/contracts";

type Props = {
  activities: Activity[];
  boardId: string;
  jobId?: string;
  filter: string;
};

const createUrl = (filter: string, boardId: string, jobId?: string) => {
  const params = new URLSearchParams();

  if (boardId) params.set("boardId", boardId);
  if (jobId) params.set("jobId", jobId);
  params.set("filter", filter);

  return `/track/create-activity${params ? `?${params}` : ""}`;
};

export const Activities = ({ activities, boardId, jobId, filter }: Props) => {
  const filterActivities = (filter: string) => {
    switch (filter) {
      case "all":
        return activities;
      case "completed":
        return activities?.filter((activity) => activity.completed === true);
      case "pending":
        return activities?.filter((activity) => activity.completed === false);
      case "applications":
        return activities?.filter((activity) => activity.type === 0);
      case "interviews":
        return activities?.filter(
          (activity) =>
            activity.type === 1 || activity.type === 2 || activity.type === 3
        );
      case "offers":
        return activities?.filter((activity) => activity.type === 4);
      case "networking":
        return activities?.filter(
          (activity) => activity.type === 16 || activity.type === 20
        );
      case "due-today":
        return activities?.filter((activity) => {
          const today = new Date();
          const activityDate = new Date(activity.createdDate);
          return (
            activityDate.getDate() === today.getDate() &&
            activityDate.getMonth() === today.getMonth() &&
            activityDate.getFullYear() === today.getFullYear()
          );
        });
      case "past-due":
        return activities?.filter((activity) => {
          const today = new Date();
          const activityDate = new Date(activity.createdDate);
          return (
            activityDate.getDate() < today.getDate() &&
            activityDate.getMonth() <= today.getMonth() &&
            activityDate.getFullYear() <= today.getFullYear()
          );
        });
      default:
        return activities;
    }
  };

  const [activeActivity, setActiveActivity] = useState<Activity | null>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      const activitiesElement = document.getElementById("activities");
      if (
        activitiesElement &&
        !activitiesElement.contains(event.target as Node)
      ) {
        setActiveActivity(null);
      }
    };

    document.addEventListener("click", handleClickOutside);

    return () => {
      document.removeEventListener("click", handleClickOutside);
    };
  }, []);

  return (
    <div className="flex flex-col gap-y-6 border-l p-5">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-2xl font-medium">Activities</h1>
        <p className="text-sm text-gray-500">View and manage activities</p>
      </div>
      <div className="flex flex-row gap-x-4">
        <Input type="text" placeholder="Search.." className="w-full max-w-xs" />
        <Button asChild>
          <Link
            href={createUrl(filter, boardId, jobId)}
            className="w-max whitespace-nowrap"
          >
            Create Activity
          </Link>
        </Button>
      </div>
      <section id="activities" className="grid w-full grid-cols-1 gap-4">
        {filterActivities(filter)?.map((activity) => (
          <Activity
            activity={activity}
            key={activity.id}
            active={activeActivity ? activeActivity.id === activity.id : false}
            setActiveActivity={setActiveActivity}
          />
        ))}
      </section>
    </div>
  );
};

function Activity({
  activity,
  active,
  setActiveActivity,
}: {
  activity: Activity;
  active: boolean;
  setActiveActivity: (activity: Activity) => void;
}) {
  const activitySchema = z.object({
    id: z.string(),
    title: z.string(),
    type: z.number(),
    startDate: z.date(),
    endDate: z.date(),
    note: z.string(),
    completed: z.boolean(),
  });

  async function onSubmit(values: UpdateActivityDetailsRequest) {
    console.log(values);
  }

  const form = useForm<UpdateActivityDetailsRequest>({
    resolver: zodResolver(activitySchema),
    defaultValues: {
      ...activity,
      startDate: new Date(activity.startDate),
      endDate: new Date(activity.endDate),
    },
  });

  console.log(form.formState.errors);

  const isFormEdited = form.formState.isDirty;

  const formVariants: Variants = {
    open: {
      padding: "1rem",
    },
    closed: {
      padding: "0.5rem",
    },
  };

  return (
    <Form {...form} key={activity.id}>
      <motion.form
        onClick={() => setActiveActivity(activity)}
        variants={formVariants}
        initial="closed"
        animate={active ? "open" : "closed"}
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex w-full flex-col  overflow-hidden rounded-md border"
      >
        <div className="flex w-full flex-row items-center gap-2">
          <FormField
            control={form.control}
            name="completed"
            render={({ field }) => (
              <FormItem className="flex flex-row items-start space-x-3 space-y-0">
                <FormControl>
                  <Checkbox
                    className="h-5 w-5"
                    checked={field.value}
                    onCheckedChange={field.onChange}
                  />
                </FormControl>
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="title"
            render={({ field }) => (
              <FormItem>
                <FormControl>
                  <Input placeholder="Title" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          {activity.job && (
            <Button
              asChild
              variant="ghost"
              className="flex flex-row justify-start gap-x-3"
            >
              <Link
                href={`/track/board/${activity.job.boardId}/job/${activity.job.id}/info`}
              >
                <Briefcase className="h-4 w-4 shrink-0" />
                <p>{activity.job.title}</p>
              </Link>
            </Button>
          )}
          <Badge className="ml-auto shrink-0">{activity.name}</Badge>
          <Badge variant="outline" className="shrink-0">
            {new Date(activity.createdDate).toDateString()}
          </Badge>
        </div>
        <AnimatePresence mode="wait">
          {active && (
            <motion.div
              initial={{
                height: 0,
                opacity: 0,
              }}
              animate={{
                height: "auto",
                opacity: 1,
                transition: {
                  height: {
                    duration: 0.4,
                  },
                  opacity: {
                    duration: 0.25,
                    delay: 0.15,
                  },
                },
              }}
              exit={{
                height: 0,
                opacity: 0,
                transition: {
                  height: {
                    duration: 0.4,
                  },
                  opacity: {
                    duration: 0.25,
                  },
                },
              }}
              className="grid gap-y-4"
            >
              <FormField
                control={form.control}
                name="note"
                render={({ field }) => (
                  <FormItem className="pt-4">
                    <FormLabel>Note</FormLabel>
                    <FormControl>
                      <Textarea placeholder="Note" {...field} />
                    </FormControl>
                    <FormMessage className="text-xs" />
                  </FormItem>
                )}
              />
              <Button
                type="submit"
                className="w-max justify-self-end px-6"
                disabled={!isFormEdited}
              >
                Save
              </Button>
            </motion.div>
          )}
        </AnimatePresence>
      </motion.form>
    </Form>
  );
}
