import { useCreateBoard } from "@/features/board";
import { fireEvent, screen, waitFor } from "@/test/test-utils";
import { useRouter } from "next/navigation";
import { renderWithProviders } from "@/test/custom-render";
import CreateBoardModalPage from "@/app/track/(modals)/create-board/page";

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
    const mockedBack = jest.fn();

    mockedUseCreateBoard.mockReturnValue({
      mutateAsync: mockedMutateAsync,
    });

    mockedUseRouter.mockReturnValue({
      back: mockedBack,
    });

    renderWithProviders(<CreateBoardModalPage />);

    waitFor(async () => {
      expect(
        await screen.findByText("View and manage boards")
      ).toBeInTheDocument();
    });

    // This will trigger the intercepting route.
    fireEvent.click(await screen.findByText("Create Board"));

    fireEvent.change(screen.getByLabelText("Name"), {
      target: { value: "My Board" },
    });

    waitFor(() => {
      expect(mockedMutateAsync).toHaveBeenCalledWith({
        name: "My Board",
      });

      expect(mockedBack).toHaveBeenCalled();
    });
  });
});
