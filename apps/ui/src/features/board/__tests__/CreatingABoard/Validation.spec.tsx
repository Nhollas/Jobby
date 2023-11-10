import {
  render,
  runInputValidationScenario,
  InputValidationScenario,
} from "@/test/test-utils";
import CreateBoardModalPage from "@/app/track/(modals)/create-board/page";
import { useCreateBoard } from "@/features/board";
import { useRouter } from "next/navigation";

jest.mock("@/features/board/api/createBoard", () => ({
  ...jest.requireActual("@/features/board/api/createBoard"),
  useCreateBoard: jest.fn(),
}));

jest.mock("next/navigation", () => ({
  useRouter: jest.fn(),
}));

const mockedUseCreateBoard = useCreateBoard as jest.Mock;
const mockedUseRouter = useRouter as jest.Mock;

describe("Create Board Modal Page Validation", () => {
  inputValidationScenarios().forEach((scenario) => {
    it(scenario.name, async () => {
      mockedUseCreateBoard.mockReturnValue({
        mutateAsync: jest.fn(),
      });

      mockedUseRouter.mockReturnValue({
        push: jest.fn(),
      });

      render(<CreateBoardModalPage />);

      runInputValidationScenario(scenario);
    });
  });
});

function inputValidationScenarios(): InputValidationScenario[] {
  return [
    {
      name: "should reject board name with less than 5 characters",
      input: "bob",
      fieldLabel: "Name",
      expectedMessage: "Board name must be at least 5 characters long",
    },
    {
      name: "should reject board name with more than 50 characters",
      input: "b".repeat(50),
      fieldLabel: "Name",
      expectedMessage: "Board name must be at most 50 characters long",
    },
    {
      name: "should reject empty board name",
      input: "",
      fieldLabel: "Name",
      expectedMessage: "Board name must not be empty",
    },
  ];
}
