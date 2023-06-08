"use client";

import { Activity } from "types";
import Link from "next/link";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { Briefcase } from "lucide-react";
import { Badge } from "@/components/ui/badge";

type Props = {
  activities: Activity[];
  boardId: string;
  jobId?: string;
};

const createUrl = (boardId?: string, jobId?: string) => {
  const params = new URLSearchParams();

  if (boardId) params.set("boardId", boardId);
  if (jobId) params.set("jobId", jobId);

  return `/create-activity${params ? `?${params}` : ""}`;
};

export const Activities = ({ activities, boardId, jobId }: Props) => {
  return (
    <div className="flex flex-col gap-y-6 overscroll-contain p-4 lg:px-8">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-2xl font-medium">Activities</h1>
        <p className="text-sm text-gray-500">View and manage activities</p>
      </div>
      <div className="flex flex-row gap-x-4">
        <Input type="text" placeholder="Search.." className="w-full max-w-xs" />
        <Button asChild>
          <Link
            href={createUrl(boardId, jobId)}
            className="w-max whitespace-nowrap"
          >
            Create Activity
          </Link>
        </Button>
      </div>
      <section className="grid w-full grid-cols-1 gap-4">
        {activities.map((activity) => (
          <Activity activity={activity} key={activity.id} />
        ))}
      </section>
    </div>
  );
};

function Activity({ activity }: { activity: Activity }) {
  const activitySchema = z.object({
    id: z.string(),
    title: z.string(),
    type: z.number(),
    startDate: z.date(),
    endDate: z.date(),
    note: z.string(),
    completed: z.boolean(),
  });

  async function onSubmit(values: z.infer<typeof activitySchema>) {
    console.log(values);
  }

  const form = useForm<z.infer<typeof activitySchema>>({
    resolver: zodResolver(activitySchema),
    defaultValues: {
      ...activity,
    },
  });

  return (
    <Form {...form} key={activity.id}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex w-full flex-row items-center gap-2 rounded-md border p-1 px-2 @container"
      >
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
              {/* <div className="space-y-1 leading-none">
                <FormLabel>Completed</FormLabel>
              </div> */}
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="title"
          render={({ field }) => (
            <FormItem>
              {/* <FormLabel>Title</FormLabel> */}
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
              href={`/board/${activity.job.boardId}/job/${activity.job.id}/info`}
            >
              <Briefcase className="h-4 w-4 shrink-0" />
              <p>{activity.job.title}</p>
            </Link>
          </Button>
        )}
        <Badge className="shrink-0">{activity.name}</Badge>
        <Badge variant="outline" className="shrink-0">
          {new Date(activity.createdDate).toDateString()}
        </Badge>
      </form>
    </Form>
  );
}
