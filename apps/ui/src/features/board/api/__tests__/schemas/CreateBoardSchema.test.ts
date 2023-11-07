import { CreateBoardSchema } from "@/features/board";
import { ValidateSchemaScenario, validateSchema } from "@/test/test-utils";

const validationScenarios: ValidateSchemaScenario[] = [
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
  validationScenarios.forEach(validateSchema);
});
