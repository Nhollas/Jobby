"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
  Calendar,
  Textarea,
  Button,
  ButtonLoading,
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  Input,
} from "@/components/ui";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { CalendarIcon } from "lucide-react";
import {
  UpdateJobDTO,
  UpdateJobSchema,
  useJobQuery,
  useUpdateJob,
} from "@/features/job";

type Props = {
  jobRef: string;
};

export function JobInfo({ jobRef }: Props) {
  const { data: jobData } = useJobQuery(jobRef);

  const { mutateAsync } = useUpdateJob();

  const form = useForm<UpdateJobDTO>({
    resolver: zodResolver(UpdateJobSchema),
    defaultValues: {
      ...jobData,
      deadline: jobData?.deadline ? new Date(jobData.deadline) : undefined,
    },
  });

  console.log("default values", form.formState.defaultValues);
  console.log("formstate", form.watch());

  const { formState } = form;
  const { isSubmitting, isDirty } = formState;

  async function onSubmit(values: UpdateJobDTO) {
    console.log(values);
    // delay by 5 secs
    await new Promise((resolve) => setTimeout(resolve, 500));

    await mutateAsync(values);
  }

  return (
    <div className="w-full">
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className="w-full max-w-lg space-y-6 p-4 @container"
        >
          <div className="grid gap-y-6 gap-x-2 @md:grid-cols-2">
            <FormField
              control={form.control}
              name="company"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Company</FormLabel>
                  <FormControl>
                    <Input placeholder="Company" {...field} />
                  </FormControl>
                  <FormMessage className="text-xs" />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="title"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Title</FormLabel>
                  <FormControl>
                    <Input placeholder="Title" {...field} />
                  </FormControl>
                  <FormMessage className="text-xs" />
                </FormItem>
              )}
            />
          </div>
          <div className="grid gap-y-6 gap-x-2 @md:grid-cols-2">
            <FormField
              control={form.control}
              name="postUrl"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Post Url</FormLabel>
                  <FormControl>
                    <Input placeholder="Url" {...field} />
                  </FormControl>
                  <FormMessage className="text-xs" />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="salary"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Salary</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Salary"
                      {...field}
                      type="number"
                      onChange={(e) => {
                        const output = parseInt(e.target.value, 10);
                        return field.onChange(isNaN(output) ? 0 : output);
                      }}
                    />
                  </FormControl>
                  <FormMessage className="text-xs" />
                </FormItem>
              )}
            />
          </div>
          <FormField
            control={form.control}
            name="description"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Description</FormLabel>
                <FormControl>
                  <Textarea placeholder="Description" {...field} />
                </FormControl>
                <FormMessage className="text-xs" />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="deadline"
            render={({ field }) => (
              <FormItem className="flex flex-col">
                <FormLabel>Deadline</FormLabel>
                <Popover>
                  <PopoverTrigger asChild>
                    <FormControl>
                      <Button
                        variant={"outline"}
                        className={cn(
                          "w-[240px] pl-3 text-left font-normal",
                          !field.value && "text-muted-foreground"
                        )}
                      >
                        {field.value ? (
                          format(field.value, "PPP")
                        ) : (
                          <span>Pick a date</span>
                        )}
                        <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                      </Button>
                    </FormControl>
                  </PopoverTrigger>
                  <PopoverContent className="w-auto p-0" align="start">
                    <Calendar
                      mode="single"
                      selected={field.value}
                      onSelect={field.onChange}
                      disabled={(date) =>
                        date.setHours(0, 0, 0, 0) <
                        new Date().setHours(0, 0, 0, 0)
                      }
                      initialFocus
                    />
                  </PopoverContent>
                </Popover>
                <FormMessage />
              </FormItem>
            )}
          />

          {isDirty && isSubmitting ? (
            <ButtonLoading />
          ) : isDirty ? (
            <Button>Update</Button>
          ) : null}
        </form>
      </Form>
    </div>
  );
}
