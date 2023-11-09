import { useCreateBoard } from "@/features/board";
import { act, fireEvent, render, screen, waitFor } from "@/test/test-utils";
import { useRouter } from "next/navigation";
import CreateBoardModalPage from "../page";

jest.mock("@/features/board/api/createBoard", () => ({
  ...jest.requireActual("@/features/board/api/createBoard"),
  useCreateBoard: jest.fn(),
}));

jest.mock("next/navigation", () => ({
  useRouter: jest.fn(),
}));

const mockedUseCreateBoard = useCreateBoard as jest.Mock;
const mockedUseRouter = useRouter as jest.Mock;

describe("Successfully Creating A Board Modal Page", () => {
  it("should successfully create a board", async () => {
    const mockedMutateAsync = jest.fn();

    mockedUseCreateBoard.mockReturnValue({
      mutateAsync: mockedMutateAsync,
    });

    mockedUseRouter.mockReturnValue({
      push: jest.fn(),
    });

    render(<CreateBoardModalPage />);

    act(() => {
      fireEvent.input(screen.getByLabelText("Name"), {
        target: { value: "My Board" },
      });

      fireEvent.submit(screen.getByText("Submit"));
    });

    waitFor(() => {
      expect(mockedMutateAsync).toHaveBeenCalledWith({
        name: "My Board",
      });

      expect(mockedUseRouter().push).toHaveBeenCalledWith("/track/boards");
    });
  });
});
