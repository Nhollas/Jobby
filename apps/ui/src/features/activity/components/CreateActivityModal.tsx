"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  Input,
  Popover,
  PopoverContent,
  PopoverTrigger,
  Button,
  Calendar,
  Textarea,
  Checkbox,
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  ScrollArea,
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
  Modal,
} from "@/components/ui";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import {
  Briefcase,
  CalendarIcon,
  Check,
  ChevronsUpDown,
  Layout,
} from "lucide-react";
import { ActivityType, activityTypesSchema } from "@/types";
import {
  CreateActivityDTO,
  CreateActivitySchema,
  useCreateActivity,
} from "@/features/activity";
import { useJobsQuery } from "@/features/job";
import { useBoardQuery } from "@/features/board";

export type ActivityFilter = keyof typeof activityFilters;

const activityFilters: Record<string, ActivityType[]> = {
  all: Object.values(activityTypesSchema.Values),
  applications: [activityTypesSchema.Values["Apply"]],
  interviews: [
    activityTypesSchema.Values["Phone Screen"],
    activityTypesSchema.Values["Phone Interview"],
    activityTypesSchema.Values["On Site Interview"],
  ],
  offers: [activityTypesSchema.Values["Offer Received"]],
  networking: [
    activityTypesSchema.Values["Meeting"],
    activityTypesSchema.Values["Networking Event"],
  ],
};

interface Props {
  filter: ActivityFilter;
  jobRef?: string;
  boardRef: string;
}

export function CreateActivityModal({ jobRef, boardRef, filter }: Props) {
  const defaultType = activityFilters[filter][0];
  const defaultTypeIndex = Object.values(activityTypesSchema.Values).indexOf(
    defaultType
  );

  const form = useForm<CreateActivityDTO>({
    resolver: zodResolver(CreateActivitySchema),
    defaultValues: {
      title: "",
      type: defaultTypeIndex,
      completed: false,
      jobReference: jobRef,
      boardReference: boardRef,
    },
  });

  const { mutateAsync } = useCreateActivity();

  const { data: jobs } = useJobsQuery();
  const { data: board } = useBoardQuery(boardRef);

  async function onSubmit(values: CreateActivityDTO) {
    await mutateAsync(values);
  }

  if (!board || !jobs) return null;

  return (
    <Modal>
      <Card className="z-50 h-full w-full max-w-lg overflow-scroll rounded-none transition-all animate-in fade-in-90 zoom-in-90 slide-in-from-bottom-10 duration-100 sm:slide-in-from-bottom-0">
        <CardHeader>
          <CardTitle>Create Activity</CardTitle>
          <CardDescription>
            Fill out info for your new activity.
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Form {...form}>
            <form
              onSubmit={form.handleSubmit(onSubmit)}
              className="space-y-6 @container"
            >
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
              <div className="grid w-full gap-y-6 gap-x-4 @md:grid-cols-2">
                <FormField
                  control={form.control}
                  name="startDate"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <FormLabel>Start Date</FormLabel>
                      <Popover>
                        <PopoverTrigger asChild>
                          <FormControl>
                            <Button
                              variant={"outline"}
                              className={cn(
                                "pl-3 text-left font-normal",
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
                <FormField
                  control={form.control}
                  name="endDate"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <FormLabel>End Date</FormLabel>
                      <Popover>
                        <PopoverTrigger asChild>
                          <FormControl>
                            <Button
                              variant={"outline"}
                              className={cn(
                                "pl-3 text-left font-normal",
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
              </div>
              <FormField
                control={form.control}
                name="type"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Type</FormLabel>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={activityFilters[filter][0]}
                    >
                      <FormControl>
                        <SelectTrigger>
                          <SelectValue placeholder="Select a verified email to display" />
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent>
                        {activityFilters[filter].map((type) => (
                          <SelectItem key={type} value={type}>
                            {type}
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
                name="note"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Note</FormLabel>
                    <FormControl>
                      <Textarea placeholder="Note" {...field} />
                    </FormControl>
                    <FormMessage className="text-xs" />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="completed"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel htmlFor="completed">Completed</FormLabel>
                    <FormControl>
                      <div className="space-y-2">
                        <div className="flex flex-row items-center gap-x-3">
                          <Checkbox
                            id="completed"
                            checked={field.value}
                            onCheckedChange={field.onChange}
                          />
                          <FormDescription>
                            Mark this activity as completed.
                          </FormDescription>
                        </div>
                      </div>
                    </FormControl>
                  </FormItem>
                )}
              />
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
                    render={() => (
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
                                  {board.name}
                                </div>
                              </Button>
                            </FormControl>
                          </PopoverTrigger>
                        </Popover>
                        <FormMessage className="text-xs" />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="jobReference"
                    render={({ field }) => (
                      <FormItem className="flex flex-col">
                        <FormLabel>Job</FormLabel>
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
                                  {field.value
                                    ? jobs.find(
                                        (job) => job.reference === field.value
                                      )?.title
                                    : "Select Job..."}
                                </p>
                                <ChevronsUpDown className="ml-auto h-4 w-4 shrink-0 opacity-50" />
                              </Button>
                            </FormControl>
                          </PopoverTrigger>
                          <PopoverContent className="p-0">
                            <Command>
                              <CommandInput placeholder="Search job..." />
                              <CommandEmpty>No Jobs Found.</CommandEmpty>
                              <CommandGroup>
                                <ScrollArea className="h-72">
                                  {jobs.map((job) => (
                                    <CommandItem
                                      key={job.reference}
                                      value={job.reference}
                                      onSelect={() => {
                                        form.setValue(
                                          "jobReference",
                                          job.reference
                                        );
                                      }}
                                    >
                                      <Briefcase className="mr-2 h-4 w-4" />
                                      <Check
                                        className={cn(
                                          "mr-2 h-4 w-4",
                                          field.value === job.reference
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
              <div className="flex flex-row gap-x-2">
                <Button type="button" variant="outline">
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
