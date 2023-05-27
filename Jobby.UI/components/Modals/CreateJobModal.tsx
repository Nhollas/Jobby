"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { Job } from "types";
import { BoardDictionaryResponse } from "types/responses/Board";
import ColourPicker from "../Common/ColourPicker";
import { useRouter } from "next/navigation";
import { useAuth } from "@clerk/nextjs";
import { Modal } from "../Modal";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../ui/card";
import { Input } from "../ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../ui/select";
import { Button } from "../ui/button";
import { z } from "zod";
import { useForm } from "react-hook-form";
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from "../ui/form";
import { Layout, List } from "lucide-react";
import { clientApi } from "@/lib/clients/clientApi";

interface Props {
  boardId: string;
  jobListId: string;
  boardsDictionary: BoardDictionaryResponse[];
}

const formSchema = z.object({
  title: z.string().nonempty(),
  company: z.string().nonempty(),
  colour: z.string(),
  jobListId: z.string(),
  boardId: z.string(),
});

export const CreateJobModal = ({
  boardId,
  jobListId,
  boardsDictionary,
}: Props) => {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: "",
      company: "",
      colour: "#ffffff",
      jobListId,
      boardId,
    },
  });

  const router = useRouter();
  const { getToken } = useAuth();

  async function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values);

    const createdJob = await clientApi.post<
      Pick<Job, "company" | "title" | "jobListId" | "boardId">,
      Job
    >("/Job/Create", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

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
              <ColourPicker
                onChange={(value) => form.setValue("colour", value)}
              />
              <FormField
                control={form.control}
                name="boardId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Board</FormLabel>
                    <Select
                      onValueChange={(value) => {
                        field.onChange(value);
                        const firstJoblistId = boardsDictionary.find(
                          (board) => board.id === value
                        )?.jobLists[0].id;

                        console.log("firstJoblistId", firstJoblistId);

                        if (firstJoblistId) {
                          form.setValue("jobListId", firstJoblistId);
                        } else {
                          form.setValue("jobListId", "");
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
                name="jobListId"
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
                                      board.id === form.getValues("boardId")
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
                            (board) => board.id === form.getValues("boardId")
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
};
