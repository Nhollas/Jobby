interface Entity {
  id: string;
  createdDate: string;
  lastUpdated: string;
}

export type JobList = {
  name: string;
  index: number;
  count: number;
  jobs: Job[];
} & Entity

export type Activity = {
  title: string;
  type: number;
  name: string;
  note: string;
  completed: boolean;
  board: Board;
  job: Job
} & Entity

export type Job = {
  company: string;
  title: string;
  postUrl: string;
  salary: number;
  location: string;
  colour: string;
  description: string;
  deadline: string;
  index: number;
  activities: Activity[]
  notes: Note[]
  contacts: Contact[]
  jobListId: string;
  boardId: string;
} & Entity

export type Note = {
  id: string;
  title: string;
  description: string;
  job: Job;
  jobId: string;
}

export type Contact = {
  firstName: string;
  lastName: string;
  jobTitle: string;
  location: string;
  socials: Social[]
  jobs: Job[]
  companies: Company[]
  emails: Email[]
  phones: Phone[]
} & Entity;

type Social = {
  twitterUrl: string;
  facebookUrl: string;
  linkedInUrl: string;
  githubUrl: string;
}

type Company = {
  id: string;
  name: string;
  contact: Contact
  contactId: string;
}

type Email = {
  id: string;
  name: string;
  contact: Contact
  contactId: string;
}

type Phone = {
  id: string;
  number: string;
  type: string;
  contact: Contact
  contactId: string;
}

export type Board = {
  id: string;
  name: string;
  createdDate: string;
  lastUpdated: string;
  activitiesCount: number;
  contactsCount: number;
  jobList: JobList[];
} & Entity