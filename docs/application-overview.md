# 💻 Application Overview

Users can create their own boards that help them keep track of job application data. This ranges from: contacts, activities, notes, dates, tasks, job descriptions, salaries, locations, company data and more.

## Frontend Data model

![Image](https://i.imgur.com/nvgbghI.png)

The application contains the following models:

```ts
/* 
    A user can have multiple boards. 
    The board serves as the top level data model.
*/
type Board = {
  reference: string;
  createdDate: Date;
  lastUpdated: Date;
  name: string;
  lists: JobList[];
};
```

```ts
/* 
    A board can have multiple job lists. 
    These lists house the their respective jobs.
*/
export type JobList = {
  name: string;
  boardReference: string;
  jobs: Job[];
} & Entity;
```

```ts
/* 
    A job list can have multiple jobs.
    A job is the main data model of the application.
*/
type Job = {
  reference: string;
  createdDate: Date;
  lastUpdated: Date;
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
  jobListReference: string;
  boardReference: string;
};
```
