"use client";

import { cn } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Building2,
  Phone,
  Mail,
  Twitter,
  Linkedin,
  Github,
  Facebook,
  Layout,
  ChevronsUpDown,
  Check,
  Briefcase,
} from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import {
  Button,
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
  Input,
  MultiInput,
  Popover,
  PopoverContent,
  PopoverTrigger,
  ScrollArea,
  FramerTabsTrigger,
  Tabs,
  TabsContent,
  TabsList,
} from "@/components/ui";
import {
  UpdateContactDTO,
  UpdateContactSchema,
  useUpdateContact,
} from "@/features/contact";
import { useBoardsQuery } from "@/features/board";
import { useJobsQuery } from "@/features/job";
import { Contact } from "@/types";

interface Props {
  contact: Contact;
}

export function Contact({ contact }: Props) {
  const { data: boards } = useBoardsQuery();
  const { data: jobs } = useJobsQuery();

  const { mutateAsync } = useUpdateContact();

  const form = useForm<UpdateContactDTO>({
    resolver: zodResolver(UpdateContactSchema),
    defaultValues: {
      ...contact,
      contactReference: contact.reference,
      jobReferences: contact.jobs.map((job) => job.reference),
      boardReference: contact.board.reference,
    },
  });

  const [filteredBoards, setFilteredBoards] = useState(boards);
  const [filteredJobs, setFilteredJobs] = useState(jobs);
  const [activeTab, setActiveTab] = useState("socials");
  const [activeTab2, setActiveTab2] = useState("companies");

  async function onSubmit(values: UpdateContactDTO) {
    await mutateAsync(values);
  }

  const isFormEdited = form.formState.isDirty;

  return (
    <Card className="h-full w-full max-w-xl overflow-scroll rounded-none border-0 shadow-none lg:max-w-none">
      <CardHeader>
        <CardTitle>View Contact</CardTitle>
        <CardDescription>Update the info on this contact.</CardDescription>
      </CardHeader>
      <CardContent className="grid grid-cols-1 gap-y-4">
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="grid grid-cols-1 gap-12 @container lg:grid-cols-2"
          >
            <section className="flex flex-col gap-y-6">
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
                        name="boardReference"
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
                                    <div className="flex w-full flex-row items-center gap-3">
                                      <Layout className="h-4 w-4 flex-shrink-0" />
                                      <p className="truncate">
                                        {field.value
                                          ? filteredBoards?.find(
                                              (board) =>
                                                board.reference === field.value
                                            )?.name
                                          : "Choose board..."}
                                      </p>
                                      <ChevronsUpDown className="h-4 w-4 shrink-0 opacity-50" />
                                    </div>
                                  </Button>
                                </FormControl>
                              </PopoverTrigger>
                              <PopoverContent className="w-72 p-0">
                                <Command>
                                  <CommandInput
                                    placeholder="Search boards..."
                                    onChangeCapture={(event) => {
                                      const inputValue =
                                        // @ts-ignore
                                        event.target.value.toLowerCase();
                                      const filteredBoards = boards?.filter(
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
                                    {filteredBoards?.map((board) => (
                                      <CommandItem
                                        key={board.reference}
                                        value={`${board.name}${board.reference}`}
                                        className="flex w-full flex-row items-center gap-2"
                                        onSelect={(value) => {
                                          const boardRef = value.substring(
                                            value.length - 36
                                          );

                                          if (boardRef === field.value) {
                                            form.setValue("boardReference", "");
                                          } else {
                                            form.setValue(
                                              "boardReference",
                                              boardRef
                                            );
                                          }
                                        }}
                                      >
                                        <Layout className="mr-2 h-4 w-4 flex-shrink-0" />
                                        <Check
                                          className={cn(
                                            "mr-2 h-4 w-4 flex-shrink-0",
                                            field.value === board.reference
                                              ? "opacity-100"
                                              : "opacity-0"
                                          )}
                                        />
                                        <p className="truncate">{board.name}</p>
                                      </CommandItem>
                                    ))}
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
                        name="jobReferences"
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
                                            .map((jobRef) => {
                                              const job = jobs?.find(
                                                (job) =>
                                                  job.reference === jobRef
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

                                      const filteredJobs = jobs?.filter((job) =>
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
                                      {filteredJobs?.map((job) => (
                                        <CommandItem
                                          key={job.reference}
                                          value={`${job.title}${job.reference}`}
                                          onSelect={(currentValue) => {
                                            const jobRef =
                                              currentValue.substring(
                                                currentValue.length - 36
                                              );

                                            let jobRefs = [...field.value];

                                            if (jobRefs.includes(jobRef)) {
                                              jobRefs = jobRefs.filter(
                                                (id) => id !== jobRef
                                              );
                                            } else {
                                              jobRefs = [
                                                ...field.value,
                                                jobRef,
                                              ];
                                            }

                                            form.setValue(
                                              "jobReferences",
                                              jobRefs
                                            );
                                          }}
                                        >
                                          <Briefcase className="mr-2 h-4 w-4" />
                                          <Check
                                            className={cn(
                                              "mr-2 h-4 w-4",
                                              field.value.includes(
                                                job.reference
                                              )
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
            </section>
            <section className="flex flex-col gap-y-6">
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
              <Button
                type="submit"
                className="w-max justify-self-end px-6"
                disabled={!isFormEdited}
              >
                Save
              </Button>
            </section>
          </form>
        </Form>
      </CardContent>
    </Card>
  );
}
