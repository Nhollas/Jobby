import { z } from "zod";

export * from "@testing-library/react";

export type ValidateSchemaScenario = {
  schema: z.ZodType;
  name: string;
  data: any;
  expectedMessage: string;
};

export function validateSchema(scenario: ValidateSchemaScenario) {
  test(`validates ${scenario.name}`, () => {
    try {
      scenario.schema.parse(scenario.data);
    } catch (error) {
      if (error instanceof z.ZodError) {
        const expectedMessage = error.errors.find(
          (val) => val.message === scenario.expectedMessage
        );

        expect(expectedMessage).toBeDefined();
      }
    }
  });
}
