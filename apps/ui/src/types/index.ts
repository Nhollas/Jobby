import { z } from "zod";

interface Entity {
  reference: string;
  createdDate: Date;
  lastUpdated: Date;
}

export type APIError = {
  status: number;
  message: string;
};

export type JobList = {
  name: string;
  boardReference: string;
  jobs: Job[];
  index?: number;
  count?: number;
} & Entity;

export type Activity = {
  title: string;
  type: number;
  name: string;
  note: string;
  completed: boolean;
  board: Board;
  job: Job;
  startDate: Date;
  endDate: Date;
} & Entity;

export type ActivityType = keyof typeof activityTypesSchema.Values;

export const activityTypesSchema = z.enum([
  "Apply",
  "Phone Screen",
  "Phone Interview",
  "On Site Interview",
  "Offer Received",
  "Accept Offer",
  "Prep Cover Letter",
  "Prep Resume",
  "Reach Out",
  "Get Reference",
  "Follow Up",
  "Prep For Interview",
  "Decline Offer",
  "Rejected",
  "Send Thank You",
  "Email",
  "Meeting",
  "Phone Call",
  "Send Availability",
  "Assignment",
  "Networking Event",
  "Application Withdrawn",
  "Other",
]);

export type Job = {
  company: string;
  title: string;
  postUrl: string;
  salary?: number;
  location?: string;
  colour: string;
  description?: string;
  deadline?: Date;
  index: number;
  activities: Activity[];
  notes: Note[];
  contacts: Contact[];
  jobListId: string;
  boardReference: string;
} & Entity;

export type Note = {
  id: string;
  title: string;
  description: string;
  job: Job;
  jobId: string;
};

export type Contact = {
  firstName: string;
  lastName: string;
  jobTitle: string;
  location: string;
  socials: Social;
  jobs: Job[];
  companies: Company[];
  emails: Email[];
  phones: Phone[];
  board: Board;
} & Entity;

export type Social = {
  twitterUrl: string;
  facebookUrl: string;
  linkedInUrl: string;
  githubUrl: string;
};

type Company = {
  id: string;
  name: string;
  contact: Contact;
  contactId: string;
};

type Email = {
  id: string;
  name: string;
  contact: Contact;
  contactId: string;
};

type Phone = {
  id: string;
  number: string;
  type: number;
  contact: Contact;
  contactId: string;
};

export type Board = {
  name: string;
  createdDate: string;
  lastUpdated: string;
  activitiesCount: number;
  contactsCount: number;
  lists: JobList[];
} & Entity;
