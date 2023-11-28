import { useCreateBoard } from "@/features/board";
import { fireEvent, screen, waitFor } from "@/test/test-utils";
import { useRouter } from "next/navigation";
import BoardsPage from "@/app/track/boards/page";
import { renderWithProviders } from "@/test/custom-render";

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

    renderWithProviders(<BoardsPage />);

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

      expect(mockedUseRouter().push).toHaveBeenCalledWith("/track/boards");
    });
  });
});
