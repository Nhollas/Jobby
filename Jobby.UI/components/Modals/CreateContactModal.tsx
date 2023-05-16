"use client";

import { useEffect, useReducer, useState } from "react";
import reducer from "reducers/CreateContactReducer";
import { postAsync } from "@/lib/clientFetch";
import { Board, Contact, Job } from "types";
import { useRouter, useSearchParams } from "next/navigation";
import { useAuth } from "@clerk/nextjs";
import { Button } from "../ui/button";
import { Label } from "../ui/label";
import { Input } from "../ui/input";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../ui/card";
import { cn } from "@/lib/utils";
import {
  Check,
  FacebookIcon,
  Github,
  Linkedin,
  Twitter,
  ChevronsUpDown,
  Briefcase,
  Layout,
  Mail,
  Phone,
  Building2,
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
import MultiInput from "../Common/MultiInput";

interface Props {
  boards: Board[];
  jobs: Job[];
}

export const CreateContactModal = ({ boards, jobs }: Props) => {
  const searchParams = useSearchParams();
  const { getToken } = useAuth();

  const boardId = searchParams.get("boardId");

  const router = useRouter();

  const [state, dispatch] = useReducer(reducer, {
    body: {
      boardId: boardId || null,
      jobIds: [],
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

  const [boardOpen, setBoardOpen] = useState(false);
  const [jobsOpen, setJobsOpen] = useState(false);
  const [filteredBoards, setFilteredBoards] = useState(boards);
  const [selectedBoardId, setSelectedBoardId] = useState("");
  const [filteredJobs, setFilteredJobs] = useState(jobs);
  const [selectedJobIds, setSelectedJobIds] = useState<string[]>([]);
  const [activeTab, setActiveTab] = useState("socials");
  const [activeTab2, setActiveTab2] = useState("companies");

  const { body } = state;

  const handleChange = (e: any) => {
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      property: e.target.name,
      value: e.target.value,
    });
  };

  const handleSocialsChange = (e: any) => {
    dispatch({
      type: "HANDLE_SOCIALS_INPUT_CHANGE",
      property: e.target.name,
      value: e.target.value,
    });
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const formattedBody = {
      ...body,
      companies: body.companies.map((company) => {
        return company.value;
      }),
      emails: body.emails.map((email) => {
        return {
          name: email.value,
          type: email.type,
        };
      }),
      phones: body.phones.map((phone) => {
        return {
          number: phone.value,
          type: phone.type,
        };
      }),
    };

    const createdContact = await postAsync<any, Contact>(
      "/contact/create",
      formattedBody,
      {
        headers: {
          Authorization: `Bearer ${await getToken()}`,
        },
      }
    );
  };

  return (
    <Modal>
      <Card className="z-50 h-full w-full max-w-lg overflow-scroll transition-all animate-in fade-in-90 zoom-in-90 slide-in-from-bottom-10 duration-100 sm:slide-in-from-bottom-0">
        <CardHeader>
          <CardTitle>Create Contact</CardTitle>
          <CardDescription>Fill out info for your new contact.</CardDescription>
        </CardHeader>
        <CardContent className="flex flex-col gap-y-4">
          <form
            method="post"
            className="flex flex-col gap-4"
            onSubmit={handleSubmit}
          >
            <div className="flex flex-row gap-x-2">
              <div className="grid w-full gap-1.5">
                <Label htmlFor="firstName" className="text-start">
                  First Name
                </Label>
                <Input
                  type="text"
                  name="firstName"
                  placeholder="Name"
                  value={body.firstName}
                  onChange={handleChange}
                />
              </div>
              <div className="grid w-full gap-1.5">
                <Label htmlFor="lastName" className="text-start">
                  Last Name
                </Label>
                <Input
                  type="text"
                  name="lastName"
                  placeholder="Name"
                  value={body.lastName}
                  onChange={handleChange}
                />
              </div>
            </div>
            <div className="flex flex-row gap-x-2">
              <div className="grid w-full gap-1.5">
                <Label htmlFor="jobTitle" className="text-start">
                  Job Title
                </Label>
                <Input
                  type="text"
                  name="jobTitle"
                  placeholder="Title"
                  value={body.jobTitle}
                  onChange={handleChange}
                />
              </div>
              <div className="grid w-full gap-1.5">
                <Label htmlFor="location" className="text-start">
                  Location
                </Label>
                <Input
                  type="text"
                  name="location"
                  placeholder="Location"
                  value={body.location}
                  onChange={handleChange}
                />
              </div>
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
                  list={body.companies}
                  name="companies"
                  label="Companies"
                  description="Add companies for this contact."
                  placeholder="Company"
                  icon={<Building2 className="h-5 rounded-lg" />}
                  onChange={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_CHANGE",
                      property: "companies",
                      value,
                    })
                  }
                  addItem={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_ADD",
                      property: "companies",
                      value,
                    })
                  }
                  removeItem={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_REMOVE",
                      property: "companies",
                      value: value,
                    })
                  }
                />
              </TabsContent>
              <TabsContent value="phones">
                <MultiInput
                  list={body.phones}
                  name="phones"
                  label="Phones"
                  description="Add phone numbers for this contact."
                  placeholder="Phone"
                  chooseType
                  icon={<Phone className="h-5 rounded-lg" />}
                  onChange={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_CHANGE",
                      property: "phones",
                      value,
                    })
                  }
                  addItem={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_ADD",
                      property: "phones",
                      value,
                    })
                  }
                  removeItem={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_REMOVE",
                      property: "phones",
                      value: value,
                    })
                  }
                />
              </TabsContent>
              <TabsContent value="emails">
                <MultiInput
                  list={body.emails}
                  name="emails"
                  label="Emails"
                  description="Add emails for this contact."
                  placeholder="Email"
                  chooseType
                  icon={<Mail className="h-5 rounded-lg" />}
                  onChange={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_CHANGE",
                      property: "emails",
                      value,
                    })
                  }
                  addItem={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_ADD",
                      property: "emails",
                      value,
                    })
                  }
                  removeItem={(value) =>
                    dispatch({
                      type: "HANDLE_ARRAY_ITEM_REMOVE",
                      property: "emails",
                      value: value,
                    })
                  }
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
                  <CardContent className="grid gap-2">
                    <div className="flex w-full flex-row items-center gap-1.5">
                      <Twitter className="h-10 w-10 rounded-lg border p-2" />
                      <Input
                        type="text"
                        name="twitterUrl"
                        placeholder="Twitter Url"
                        value={body.socials.twitterUrl}
                        onChange={handleSocialsChange}
                      />
                    </div>
                    <div className="flex w-full flex-row items-center gap-1.5">
                      <Linkedin className="h-10 w-10 rounded-lg border p-2" />
                      <Input
                        type="text"
                        name="linkedInUrl"
                        placeholder="LinkedIn Url"
                        value={body.socials.linkedInUrl}
                        onChange={handleSocialsChange}
                      />
                    </div>
                    <div className="flex w-full flex-row items-center gap-1.5">
                      <Github className="h-10 w-10 rounded-lg border p-2" />
                      <Input
                        type="text"
                        name="githubUrl"
                        placeholder="Github Url"
                        value={body.socials.githubUrl}
                        onChange={handleSocialsChange}
                      />
                    </div>
                    <div className="flex w-full flex-row items-center gap-1.5">
                      <FacebookIcon className="h-10 w-10 rounded-lg border p-2" />
                      <Input
                        type="text"
                        name="facebookUrl"
                        placeholder="Facebook Url"
                        value={body.socials.facebookUrl}
                        onChange={handleSocialsChange}
                      />
                    </div>
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
                    <Popover open={boardOpen} onOpenChange={setBoardOpen}>
                      <div className="flex w-full flex-col items-start gap-1.5">
                        <Label htmlFor="board" className="text-start">
                          Board
                        </Label>
                        <PopoverTrigger asChild>
                          <Button
                            variant="outline"
                            role="combobox"
                            aria-expanded={boardOpen}
                            className="w-full justify-between"
                          >
                            <div className="flex flex-row items-center gap-3">
                              <Layout className="h-4 w-4" />
                              {selectedBoardId
                                ? filteredBoards.find(
                                    (board) => board.id === selectedBoardId
                                  )?.name
                                : "Choose board..."}
                            </div>
                            <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                          </Button>
                        </PopoverTrigger>
                      </div>
                      <PopoverContent className="p-0">
                        <Command>
                          <CommandInput
                            placeholder="Search board..."
                            onChangeCapture={(event) => {
                              const inputValue =
                                // @ts-ignore
                                event.target.value.toLowerCase();
                              const filteredBoards = boards.filter((board) =>
                                board.name.toLowerCase().includes(inputValue)
                              );

                              setFilteredBoards(filteredBoards);
                            }}
                          />
                          <CommandEmpty>No boards found.</CommandEmpty>
                          <CommandGroup>
                            <ScrollArea className="h-72">
                              {filteredBoards.map((board) => (
                                <CommandItem
                                  key={board.id}
                                  value={`${board.name}${board.id}`}
                                  onSelect={(currentValue) => {
                                    const boardId = currentValue.substring(
                                      currentValue.length - 36
                                    );

                                    setSelectedBoardId(
                                      boardId === selectedBoardId ? "" : boardId
                                    );

                                    dispatch({
                                      type: "HANDLE_INPUT_CHANGE",
                                      property: "boardId",
                                      value: boardId,
                                    });

                                    setBoardOpen(false);
                                  }}
                                >
                                  <Layout className="mr-2 h-4 w-4" />
                                  <Check
                                    className={cn(
                                      "mr-2 h-4 w-4",
                                      selectedBoardId === board.id
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
                    <Popover open={jobsOpen} onOpenChange={setJobsOpen}>
                      <div className="flex w-full flex-col items-start gap-1.5">
                        <Label htmlFor="board" className="text-start">
                          Jobs
                        </Label>
                        <PopoverTrigger asChild>
                          <Button
                            variant="outline"
                            role="combobox"
                            aria-expanded={jobsOpen}
                            className="relative w-full justify-between overflow-hidden"
                          >
                            <div className="absolute flex flex-row items-center gap-3 truncate">
                              <Briefcase className="h-4 w-4" />
                              {selectedJobIds.length === 0
                                ? "Select Jobs..."
                                : selectedJobIds
                                    .map((jobId) => {
                                      const job = jobs.find(
                                        (job) => job.id === jobId
                                      );

                                      return job?.title;
                                    })
                                    .join(", ")}
                            </div>
                          </Button>
                        </PopoverTrigger>
                      </div>
                      <PopoverContent className="w-full p-0">
                        <Command>
                          <CommandInput
                            placeholder="Search job..."
                            onChangeCapture={(event) => {
                              const inputValue =
                                // @ts-ignore
                                event.target.value.toLowerCase();

                              const filteredJobs = jobs.filter((job) =>
                                job.title.toLowerCase().includes(inputValue)
                              );

                              setFilteredJobs(filteredJobs);
                            }}
                          />

                          <CommandEmpty>No Jobs found.</CommandEmpty>
                          <CommandGroup>
                            <ScrollArea className="h-72">
                              {filteredJobs.map((job) => (
                                <CommandItem
                                  key={job.id}
                                  value={`${job.title}${job.id}`}
                                  onSelect={(currentValue) => {
                                    const jobId = currentValue.substring(
                                      currentValue.length - 36
                                    );

                                    let jobIds = [...selectedJobIds];

                                    if (jobIds.includes(jobId)) {
                                      jobIds = jobIds.filter(
                                        (id) => id !== jobId
                                      );
                                    } else {
                                      jobIds = [...selectedJobIds, jobId];
                                    }

                                    setSelectedJobIds(jobIds);

                                    dispatch({
                                      type: "HANDLE_INPUT_CHANGE",
                                      property: "jobIds",
                                      value: jobIds,
                                    });
                                  }}
                                >
                                  <Briefcase className="mr-2 h-4 w-4" />
                                  <Check
                                    className={cn(
                                      "mr-2 h-4 w-4",
                                      selectedJobIds.includes(job.id)
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
                  </CardContent>
                </Card>
              </TabsContent>
            </Tabs>
          </form>
        </CardContent>
        <CardFooter className="gap-2">
          <Button type="button" variant="outline" onClick={router.back}>
            Cancel
          </Button>
          <Button type="submit" variant="default" onClick={handleSubmit}>
            Create
          </Button>
        </CardFooter>
      </Card>
    </Modal>
  );
};
