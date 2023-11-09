import { Board, Job, JobList } from "@/types";
import { faker } from "@faker-js/faker";

// This will generate something like "bo_fake_1a2b3c4"
function referenceGenerator(typeName: string): string {
  const prefix = typeName.slice(0, 2).toLowerCase();
  const randomString = faker.string.alphanumeric(7);
  const reference = `${prefix}_fake_${randomString}`;
  return reference;
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

type FlatBoardOverrides = {
  lists?: FlatJobListOverrides;
} & Partial<Omit<Board, "lists">>;

type FlatJobListOverrides = {
  jobs?: FlatJobOverrides;
} & Partial<Omit<JobList, "jobs">>;

type FlatJobOverrides = Partial<Job>;

type NestedBoardOverrides = {
  lists?: NestedJobListOverrides[];
} & Partial<Omit<Board, "lists">>;

type NestedJobListOverrides = {
  jobs?: NestedJobOverrides[];
} & Partial<Omit<JobList, "jobs">>;

type NestedJobOverrides = Partial<Job>;

// Implementation
export function boardGenerator(
  boardOverrides?: FlatBoardOverrides | NestedBoardOverrides
): Board {
  const boardRef = referenceGenerator("Board");

  const handleFlatOrNestedListOverrides = (
    jobListOverrides?: FlatJobListOverrides | NestedJobListOverrides[]
  ): JobList[] => {
    // Check for old flat syntax and convert to array
    const jobListOverridesArray = Array.isArray(jobListOverrides)
      ? jobListOverrides
      : Array.from(
          { length: faker.number.int({ min: 8, max: 16 }) },
          () => jobListOverrides
        );

    return jobListOverridesArray.map((jobListOverrides) =>
      jobListGenerator(jobListOverrides)
    );
  };

  const jobListGenerator = (
    jobListOverrides?: FlatJobListOverrides | NestedJobListOverrides
  ): JobList => {
    const jobListRef = referenceGenerator("JobList");

    const handleFlatOrNestedJobOverrides = (
      jobOverrides?: FlatJobOverrides | NestedJobOverrides[]
    ): Job[] => {
      // Check for old flat syntax and convert to array
      const jobOverridesArray = Array.isArray(jobOverrides)
        ? jobOverrides
        : Array.from(
            { length: faker.number.int({ min: 2, max: 5 }) },
            () => jobOverrides
          );

      return jobOverridesArray.map((jobOverrides, index) =>
        jobGenerator({ ...jobOverrides, index })
      );
    };

    const jobGenerator = (
      jobOverrides?: FlatJobOverrides | NestedJobOverrides
    ): Job => {
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
        index: 0,
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
      index: faker.number.int(100),
      name: faker.lorem.words(2),
      ...jobListOverrides,
      jobs: handleFlatOrNestedJobOverrides(jobListOverrides?.jobs),
    };
  };

  return {
    reference: boardRef,
    name: faker.lorem.words(2),
    createdDate: faker.date.recent(),
    lastUpdated: faker.date.recent(),
    ...boardOverrides,
    lists: handleFlatOrNestedListOverrides(boardOverrides?.lists),
  };
}
