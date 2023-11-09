import { Board, Job, JobList } from "@/types";
import { faker } from "@faker-js/faker";
import util from "util";

// This will generate something like "bo_fake_1a2b3c4"
function referenceGenerator(typeName: string): string {
  const prefix = typeName.slice(0, 2).toLowerCase();
  const randomString = faker.string.alphanumeric(7);
  return `${prefix}_fake_${randomString}`;
}

function colourGenerator(): string {
  return faker.helpers.arrayElement([
    "#fb923c",
    "#f87171",
    "#fbbf24",
    "#a3e635",
    "#34d399",
    "#22d3ee",
    "#818cf8",
  ]);
}

type BoardOverrides = {
  lists?: JobListOverrides[];
} & Partial<Omit<Board, "lists">>;

type JobListOverrides = {
  jobs?: JobOverrides[];
} & Partial<Omit<JobList, "jobs">>;

type JobOverrides = {} & Partial<Job>;

// Recursively generate a board with job lists and jobs...
export const boardGenerator = (overrides?: BoardOverrides): Board => {
  const boardRef = referenceGenerator("Board");

  const jobListGenerator = (jobListOverrides?: JobListOverrides): JobList => {
    const jobListRef = referenceGenerator("JobList");

    const jobGenerator = (jobOverrides?: JobOverrides): Job => {
      return {
        reference: referenceGenerator("Job"),
        activities: [],
        contacts: [],
        colour: colourGenerator(),
        company: faker.company.name(),
        notes: [],
        title: faker.lorem.words(2),
        location: faker.location.city(),
        description: faker.lorem.paragraph(),
        salary: faker.number.int({ min: 10000, max: 90000 }),
        postUrl: faker.internet.url(),
        index: 1,
        lastUpdated: faker.date.recent(),
        createdDate: faker.date.recent(),
        deadline: faker.date.future(),
        jobListReference: jobListRef,
        boardReference: boardRef,
        ...jobOverrides,
      };
    };

    return {
      reference: jobListRef,
      createdDate: faker.date.recent(),
      lastUpdated: faker.date.recent(),
      boardReference: boardRef,
      count: faker.number.int(100),
      index: faker.number.int(100),
      name: faker.lorem.words(2),
      ...jobListOverrides,
      jobs: (
        jobListOverrides?.jobs ||
        Array.from({ length: faker.number.int({ min: 2, max: 5 }) })
      ).map((jobOverrides) => {
        return jobGenerator(jobOverrides);
      }),
    };
  };

  return {
    reference: boardRef,
    name: faker.lorem.words(2),
    createdDate: faker.date.recent(),
    lastUpdated: faker.date.recent(),
    ...overrides,
    lists: (
      overrides?.lists ||
      Array.from({ length: faker.number.int({ min: 1, max: 5 }) })
    ).map((jobListOverrides) => {
      return jobListGenerator(jobListOverrides);
    }),
  };
};

it("generates a board with job lists and jobs", () => {
  const board = boardGenerator({
    lists: [
      {
        name: "Applied",
        jobs: [
          {
            company: "Google",
          },
          // Default overrides for job 2 and job 3
          {
            company: "Facebook",
          },
          {
            company: "Amazon",
          },
        ],
      },
      // Default overrides for jobList 2
      {},
      // Default overrides for jobList 3
      {},
    ],
  });

  console.log(
    util.inspect(board, { showHidden: false, depth: null, colors: true })
  );
});
