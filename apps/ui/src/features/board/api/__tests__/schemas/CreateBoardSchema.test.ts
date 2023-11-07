import { CreateBoardSchema } from "@/features/board";
import { SchemaScenario, validateSchemaScenario } from "@/test/test-utils";

const schemaScenarios: SchemaScenario[] = [
  {
    name: "should reject board name with less than 5 characters",
    data: { name: "bob" },
    expectedMessage: "Board name must be at least 5 characters long",
    schema: CreateBoardSchema,
  },
  {
    name: "should reject board name with more than 50 characters",
    data: { name: "b".repeat(50) },
    expectedMessage: "Board name must be at most 50 characters long",
    schema: CreateBoardSchema,
  },
  {
    name: "should reject empty board name",
    data: { name: "" },
    expectedMessage: "Board name must not be empty",
    schema: CreateBoardSchema,
  },
];

describe("CreateBoardSchema validation cases:", () => {
  schemaScenarios.forEach(validateSchemaScenario);
});
