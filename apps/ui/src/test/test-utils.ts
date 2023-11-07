import { act, fireEvent, screen, waitFor } from "@testing-library/react";
import { z } from "zod";
import "@testing-library/jest-dom";

export * from "@testing-library/react";

export type SchemaScenario = {
  schema: z.ZodType;
  name: string;
  data: any;
  expectedMessage: string;
};

export function validateSchemaScenario(scenario: SchemaScenario) {
  it(`validates ${scenario.name}`, () => {
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

export type InputValidationScenario = {
  name: string;
  fieldLabel: string;
  input: string;
  expectedMessage: string;
};

export function runInputValidationScenario(scenario: InputValidationScenario) {
  act(() => {
    fireEvent.input(screen.getByPlaceholderText(scenario.fieldLabel), {
      target: { value: scenario.input },
    });

    fireEvent.submit(screen.getByText("Submit"));
  });
  waitFor(() => {
    expect(screen.getByText(scenario.expectedMessage)).toBeInTheDocument();
    expect(screen.getByText("Submit")).toBeDisabled();
  });
}
