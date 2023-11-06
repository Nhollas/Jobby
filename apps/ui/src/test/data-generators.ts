import { Board, JobList } from "@/types";
import { faker } from "@faker-js/faker";

// This will generate something like "bo_fake_1a2b3c4"
function referenceGenerator(typeName: string): string {
  const prefix = typeName.slice(0, 2).toLowerCase();
  const randomString = faker.string.alphanumeric(7);
  const reference = `${prefix}_fake_${randomString}`;
  return reference;
}

export const boardGenerator = (overrides?: Partial<Board>): Board => {
  const reference = referenceGenerator("Board");

  return {
    reference: reference,
    createdDate: faker.date.recent(),
    lastUpdated: faker.date.recent(),
    lists: Array.from({ length: faker.number.int({ min: 1, max: 5 }) }, () =>
      jobListGenerator({ boardReference: reference })
    ),
    name: faker.lorem.words(2),
    ...overrides,
  };
};

export const jobListGenerator = (overrides?: Partial<JobList>): JobList => ({
  reference: referenceGenerator("JobList"),
  createdDate: faker.date.recent(),
  lastUpdated: faker.date.recent(),
  boardReference: referenceGenerator("Board"),
  count: faker.number.int(100),
  index: faker.number.int(100),
  jobs: [],
  name: faker.lorem.words(2),
  ...overrides,
});
