"use client";

import { useState } from "react";
import { Board, Job } from "types";
import { useRouter, useSearchParams } from "next/navigation";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../ui/card";
import { cn } from "@/lib/utils";
import {
  Check,
  Github,
  Linkedin,
  Twitter,
  ChevronsUpDown,
  Briefcase,
  Layout,
  Facebook,
  Building2,
  Phone,
  Mail,
} from "lucide-react";

import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
} from "@/components/ui/command";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { FramerTabsTrigger, Tabs, TabsContent, TabsList } from "../ui/tabs";
import React from "react";
import { ScrollArea } from "../ui/scroll-area";
import { Modal } from "../Modal";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from "../ui/form";
import MultiInput from "../ui/multi-input";
import { useCreateContact } from "@/hooks/useContactData";

interface Props {
  boards: Board[];
  jobs: Job[];
}

const formSchema = z.object({
  boardId: z.string().nullable(),
  jobIds: z.array(z.string()),
  firstName: z
    .string()
    .nonempty({ message: "The First Name field is required." }),
  lastName: z
    .string()
    .nonempty({ message: "The Last Name field is required." }),
  jobTitle: z.string().nonempty({ message: "The Title field is required." }),
  location: z.string(),
  socials: z.object({
    twitterUrl: z.string().url().or(z.literal("")),
    facebookUrl: z.string().url().or(z.literal("")),
    linkedInUrl: z.string().url().or(z.literal("")),
    githubUrl: z.string().url().or(z.literal("")),
  }),
  emails: z.array(
    z.object({
      name: z.string().email({ message: "Invalid email address." }),
      type: z.string().transform((val) => parseInt(val)),
    })
  ),
  phones: z.array(
    z.object({
      number: z.string(),
      type: z.string().transform((val) => parseInt(val)),
    })
  ),
  companies: z
    .array(
      z.object({
        value: z
          .string()
          .nonempty({ message: "The Company field is required." }),
      })
    )
    .transform((val) => val.map((v) => v.value)),
});

export const CreateContactModal = ({ boards, jobs }: Props) => {
  const searchParams = useSearchParams();

  const boardId = searchParams.get("boardId");
  const jobId = searchParams.get("jobId");

  const router = useRouter();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      boardId: boardId || null,
      jobIds: [jobId || undefined],
      firstName: "",
      lastName: "",
      jobTitle: "",
      location: "",
      socials: {
        twitterUrl: "",
        facebookUrl: "",
        linkedInUrl: "",
        githubUrl: "",
      },
      emails: [],
      phones: [],
      companies: [],
    },
  });

  const [filteredBoards, setFilteredBoards] = useState(boards);
  const [filteredJobs, setFilteredJobs] = useState(jobs);
  const [activeTab, setActiveTab] = useState("socials");
  const [activeTab2, setActiveTab2] = useState("companies");

  const { mutateAsync } = useCreateContact();

  async function onSubmit(values: z.infer<typeof formSchema>) {
    await mutateAsync(values);

    router.back();
  }

  return (
    <Modal>
      <Card className="z-50 h-full w-full max-w-lg overflow-scroll transition-all animate-in fade-in-90 zoom-in-90 slide-in-from-bottom-10 duration-100 sm:slide-in-from-bottom-0">
        <CardHeader>
          <CardTitle>Create Contact</CardTitle>
          <CardDescription>Fill out info for your new contact.</CardDescription>
        </CardHeader>
        <CardContent className="flex flex-col gap-y-4">
          <Form {...form}>
            <form
              onSubmit={form.handleSubmit(onSubmit)}
              className="space-y-6 @container"
            >
              <div className="grid gap-y-6 gap-x-2 @md:grid-cols-2">
                <FormField
                  control={form.control}
                  name="firstName"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>First Name</FormLabel>
                      <FormControl>
                        <Input placeholder="First Name" {...field} />
                      </FormControl>
                      <FormMessage className="text-xs" />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="lastName"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Last Name</FormLabel>
                      <FormControl>
                        <Input placeholder="Last Name" {...field} />
                      </FormControl>
                      <FormMessage className="text-xs" />
                    </FormItem>
                  )}
                />
              </div>
              <div className="grid gap-y-6 gap-x-2 @md:grid-cols-2">
                <FormField
                  control={form.control}
                  name="jobTitle"
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
                <FormField
                  control={form.control}
                  name="location"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Location</FormLabel>
                      <FormControl>
                        <Input placeholder="Location" {...field} />
                      </FormControl>
                      <FormMessage className="text-xs" />
                    </FormItem>
                  )}
                />
              </div>
              <Tabs
                onValueChange={(value) => setActiveTab2(value)}
                defaultValue="companies"
                className="flex w-full flex-col gap-y-2"
              >
                <TabsList className="grid w-full grid-cols-3">
                  <FramerTabsTrigger
                    value="companies"
                    className="z-10 w-full"
                    active={activeTab2 === "companies"}
                    layoutId={"2"}
                  >
                    Companies
                  </FramerTabsTrigger>
                  <FramerTabsTrigger
                    value="phones"
                    className="z-10 w-full"
                    active={activeTab2 === "phones"}
                    layoutId={"2"}
                  >
                    Phones
                  </FramerTabsTrigger>
                  <FramerTabsTrigger
                    value="emails"
                    className="z-10 w-full"
                    active={activeTab2 === "emails"}
                    layoutId={"2"}
                  >
                    Emails
                  </FramerTabsTrigger>
                </TabsList>
                <TabsContent value="companies">
                  <MultiInput
                    control={form.control}
                    name="companies"
                    label="Companies"
                    description="Add companies to your contact."
                    icon={<Building2 className="h-5 w-5 flex-shrink-0" />}
                    propertyName="value"
                  />
                </TabsContent>
                <TabsContent value="phones">
                  <MultiInput
                    control={form.control}
                    name="phones"
                    label="Phones"
                    description="Add phones to your contact."
                    icon={<Phone className="h-5 w-5 flex-shrink-0" />}
                    propertyName="number"
                    includeType
                  />
                </TabsContent>
                <TabsContent value="emails">
                  <MultiInput
                    control={form.control}
                    name="emails"
                    label="Emails"
                    description="Add emails to your contact."
                    icon={<Mail className="h-5 w-5 flex-shrink-0" />}
                    propertyName="name"
                    includeType
                  />
                </TabsContent>
              </Tabs>
              <Tabs
                onValueChange={(value) => setActiveTab(value)}
                defaultValue="socials"
                className="flex w-full flex-col gap-y-2"
              >
                <TabsList className="grid w-full grid-cols-2">
                  <FramerTabsTrigger
                    value="socials"
                    className="z-10 w-full"
                    active={activeTab === "socials"}
                    layoutId={"1"}
                  >
                    Socials
                  </FramerTabsTrigger>
                  <FramerTabsTrigger
                    value="linkedTo"
                    className="z-10 w-full"
                    active={activeTab === "linkedTo"}
                    layoutId={"1"}
                  >
                    Link To
                  </FramerTabsTrigger>
                </TabsList>
                <TabsContent value="socials">
                  <Card>
                    <CardHeader>
                      <CardTitle>Socials</CardTitle>
                      <CardDescription>
                        Add social links for this contact.
                      </CardDescription>
                    </CardHeader>
                    <CardContent className="flex w-full flex-col gap-2">
                      <FormField
                        control={form.control}
                        name="socials.twitterUrl"
                        render={({ field }) => (
                          <FormItem>
                            <FormControl>
                              <div className="flex w-full flex-row items-center gap-1.5">
                                <FormLabel>
                                  <Twitter className="h-10 w-10 rounded-lg border p-2" />
                                </FormLabel>
                                <Input
                                  placeholder="Twitter Url"
                                  className="w-full"
                                  {...field}
                                />
                              </div>
                            </FormControl>
                            <FormMessage className="text-xs" />
                          </FormItem>
                        )}
                      />
                      <FormField
                        control={form.control}
                        name="socials.linkedInUrl"
                        render={({ field }) => (
                          <FormItem>
                            <FormControl>
                              <div className="flex w-full flex-row items-center gap-1.5">
                                <FormLabel>
                                  <Linkedin className="h-10 w-10 rounded-lg border p-2" />
                                </FormLabel>
                                <Input placeholder="LinkedIn Url" {...field} />
                              </div>
                            </FormControl>
                            <FormMessage className="text-xs" />
                          </FormItem>
                        )}
                      />
                      <FormField
                        control={form.control}
                        name="socials.githubUrl"
                        render={({ field }) => (
                          <FormItem>
                            <FormControl>
                              <div className="flex w-full flex-row items-center gap-1.5">
                                <FormLabel>
                                  <Github className="h-10 w-10 rounded-lg border p-2" />
                                </FormLabel>
                                <Input placeholder="Github Url" {...field} />
                              </div>
                            </FormControl>
                            <FormMessage className="text-xs" />
                          </FormItem>
                        )}
                      />
                      <FormField
                        control={form.control}
                        name="socials.facebookUrl"
                        render={({ field }) => (
                          <FormItem>
                            <FormControl>
                              <div className="flex w-full flex-row items-center gap-1.5">
                                <FormLabel>
                                  <Facebook className="h-10 w-10 rounded-lg border p-2" />
                                </FormLabel>
                                <Input placeholder="Facebook Url" {...field} />
                              </div>
                            </FormControl>
                            <FormMessage className="text-xs" />
                          </FormItem>
                        )}
                      />
                    </CardContent>
                  </Card>
                </TabsContent>
                <TabsContent value="linkedTo">
                  <Card className="w-full">
                    <CardHeader>
                      <CardTitle>Link To</CardTitle>
                      <CardDescription>
                        Optionally link this contact to many jobs or a board.
                      </CardDescription>
                    </CardHeader>
                    <CardContent className="flex flex-col gap-y-4">
                      <FormField
                        control={form.control}
                        name="boardId"
                        render={({ field }) => (
                          <FormItem className="flex flex-col">
                            <FormLabel>Board</FormLabel>
                            <Popover>
                              <PopoverTrigger asChild>
                                <FormControl>
                                  <Button
                                    variant="outline"
                                    role="combobox"
                                    className="w-full justify-between"
                                  >
                                    <div className="flex flex-row items-center gap-3">
                                      <Layout className="h-4 w-4" />
                                      {field.value
                                        ? filteredBoards.find(
                                            (board) => board.id === field.value
                                          )?.name
                                        : "Choose board..."}
                                    </div>
                                    <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                  </Button>
                                </FormControl>
                              </PopoverTrigger>
                              <PopoverContent className="p-0">
                                <Command>
                                  <CommandInput
                                    placeholder="Search boards..."
                                    onChangeCapture={(event) => {
                                      const inputValue =
                                        // @ts-ignore
                                        event.target.value.toLowerCase();
                                      const filteredBoards = boards.filter(
                                        (board) =>
                                          board.name
                                            .toLowerCase()
                                            .includes(inputValue)
                                      );

                                      setFilteredBoards(filteredBoards);
                                    }}
                                  />
                                  <CommandEmpty>No Boards Found.</CommandEmpty>
                                  <CommandGroup>
                                    <ScrollArea className="h-72">
                                      {filteredBoards.map((board) => (
                                        <CommandItem
                                          key={board.id}
                                          value={`${board.name}${board.id}`}
                                          onSelect={(value) => {
                                            const boardId = value.substring(
                                              value.length - 36
                                            );

                                            if (boardId === field.value) {
                                              form.setValue("boardId", null);
                                            } else {
                                              form.setValue("boardId", boardId);
                                            }
                                          }}
                                        >
                                          <Layout className="mr-2 h-4 w-4" />
                                          <Check
                                            className={cn(
                                              "mr-2 h-4 w-4",
                                              field.value === board.id
                                                ? "opacity-100"
                                                : "opacity-0"
                                            )}
                                          />
                                          {board.name}
                                        </CommandItem>
                                      ))}
                                    </ScrollArea>
                                  </CommandGroup>
                                </Command>
                              </PopoverContent>
                            </Popover>
                            <FormMessage className="text-xs" />
                          </FormItem>
                        )}
                      />
                      <FormField
                        control={form.control}
                        name="jobIds"
                        render={({ field }) => (
                          <FormItem className="flex flex-col">
                            <FormLabel>Jobs</FormLabel>
                            <Popover>
                              <PopoverTrigger asChild>
                                <FormControl>
                                  <Button
                                    variant="outline"
                                    role="combobox"
                                    className="flex w-full flex-row justify-start gap-x-3"
                                  >
                                    <Briefcase className="h-4 w-4 shrink-0" />
                                    <p className="truncate">
                                      {field.value.length === 0
                                        ? "Select Jobs..."
                                        : field.value
                                            .map((jobId) => {
                                              const job = jobs.find(
                                                (job) => job.id === jobId
                                              );

                                              return job?.title;
                                            })
                                            .join(", ")}
                                    </p>
                                    <ChevronsUpDown className="ml-auto h-4 w-4 shrink-0 opacity-50" />
                                  </Button>
                                </FormControl>
                              </PopoverTrigger>
                              <PopoverContent className="p-0">
                                <Command>
                                  <CommandInput
                                    placeholder="Search jobs..."
                                    onChangeCapture={(event) => {
                                      const inputValue =
                                        // @ts-ignore
                                        event.target.value.toLowerCase();

                                      const filteredJobs = jobs.filter((job) =>
                                        job.title
                                          .toLowerCase()
                                          .includes(inputValue)
                                      );

                                      setFilteredJobs(filteredJobs);
                                    }}
                                  />
                                  <CommandEmpty>No Jobs Found.</CommandEmpty>
                                  <CommandGroup>
                                    <ScrollArea className="h-72">
                                      {filteredJobs.map((job) => (
                                        <CommandItem
                                          key={job.id}
                                          value={`${job.title}${job.id}`}
                                          onSelect={(currentValue) => {
                                            const jobId =
                                              currentValue.substring(
                                                currentValue.length - 36
                                              );

                                            let jobIds = [...field.value];

                                            if (jobIds.includes(jobId)) {
                                              jobIds = jobIds.filter(
                                                (id) => id !== jobId
                                              );
                                            } else {
                                              jobIds = [...field.value, jobId];
                                            }

                                            console.log(jobIds);

                                            form.setValue("jobIds", jobIds);
                                          }}
                                        >
                                          <Briefcase className="mr-2 h-4 w-4" />
                                          <Check
                                            className={cn(
                                              "mr-2 h-4 w-4",
                                              field.value.includes(job.id)
                                                ? "opacity-100"
                                                : "opacity-0"
                                            )}
                                          />
                                          <div>
                                            <h2 className="text-sm font-semibold leading-none tracking-tight">
                                              {job.title}
                                            </h2>
                                            <p className="text-xs text-muted-foreground">
                                              {job.company}
                                            </p>
                                          </div>
                                        </CommandItem>
                                      ))}
                                    </ScrollArea>
                                  </CommandGroup>
                                </Command>
                              </PopoverContent>
                            </Popover>
                            <FormMessage className="text-xs" />
                          </FormItem>
                        )}
                      />
                    </CardContent>
                  </Card>
                </TabsContent>
              </Tabs>
              <div className="flex flex-row gap-x-2">
                <Button type="button" variant="outline" onClick={router.back}>
                  Cancel
                </Button>
                <Button>Submit</Button>
              </div>
            </form>
          </Form>
        </CardContent>
      </Card>
    </Modal>
  );
};
