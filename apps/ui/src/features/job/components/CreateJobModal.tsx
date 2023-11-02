"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { Modal } from "@/components/Modal";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
  Input,
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
  Button,
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from "@/components/ui";
import { useForm } from "react-hook-form";
import { Layout, List } from "lucide-react";
import { CreateJobDTO, CreateJobSchema, useCreateJob } from "@/features/job";

interface Props {
  boardRef: string;
  jobListRef: string;
  boardsDictionary: any;
}

export function CreateJobModal({
  boardRef,
  jobListRef,
  boardsDictionary,
}: Props) {
  const form = useForm<CreateJobDTO>({
    resolver: zodResolver(CreateJobSchema),
    defaultValues: {
      title: "",
      company: "",
      boardReference: boardRef,
      jobListReference: jobListRef,
    },
  });

  const router = useRouter();

  const { mutateAsync } = useCreateJob();

  async function onSubmit(values: CreateJobDTO) {
    await mutateAsync(values);

    router.back();
  }

  return (
    <Modal>
      <Card className="z-50 h-full w-full max-w-lg overflow-scroll transition-all animate-in fade-in-90 zoom-in-90 slide-in-from-bottom-10 duration-100 sm:slide-in-from-bottom-0">
        <CardHeader>
          <CardTitle>Create Job</CardTitle>
          <CardDescription>Fill out info for your new job.</CardDescription>
        </CardHeader>
        <CardContent className="flex flex-col gap-y-4">
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
              <FormField
                control={form.control}
                name="title"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Title</FormLabel>
                    <FormControl>
                      <Input placeholder="Title" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="company"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Company</FormLabel>
                    <FormControl>
                      <Input placeholder="Company" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="boardReference"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Board</FormLabel>
                    <Select
                      onValueChange={(value) => {
                        field.onChange(value);
                        const firstJoblistId = boardsDictionary.find(
                          (board) => board.id === value
                        )?.jobLists[0].id;

                        if (firstJoblistId) {
                          form.setValue("jobListReference", firstJoblistId);
                        } else {
                          form.setValue("jobListReference", "");
                        }
                      }}
                      defaultValue={field.value}
                    >
                      <FormControl>
                        <SelectTrigger>
                          <div className="flex flex-row items-center">
                            <Layout className="mr-2 h-4 w-4" />
                            <SelectValue placeholder="Select a board" />
                          </div>
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent>
                        {boardsDictionary.map((board) => (
                          <SelectItem key={board.id} value={board.id}>
                            {board.name}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="jobListReference"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Joblist</FormLabel>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={field.value}
                    >
                      <FormControl>
                        <SelectTrigger>
                          <div className="flex flex-row items-center">
                            <List className="mr-2 h-4 w-4" />
                            <span>
                              {
                                boardsDictionary
                                  .find(
                                    (board) =>
                                      board.id ===
                                      form.getValues("boardReference")
                                  )
                                  ?.jobLists.find(
                                    (joblist) => joblist.id === field.value
                                  )?.name
                              }
                            </span>
                          </div>
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent>
                        {boardsDictionary
                          .find(
                            (board) =>
                              board.id === form.getValues("boardReference")
                          )
                          ?.jobLists.map((joblist) => (
                            <SelectItem key={joblist.id} value={joblist.id}>
                              {joblist.name}
                            </SelectItem>
                          ))}
                      </SelectContent>
                    </Select>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <div className="flex flex-row gap-x-2">
                <Button type="button" variant="outline" onClick={router.back}>
                  Cancel
                </Button>
                <Button type="submit">Submit</Button>
              </div>
            </form>
          </Form>
        </CardContent>
      </Card>
    </Modal>
  );
}
