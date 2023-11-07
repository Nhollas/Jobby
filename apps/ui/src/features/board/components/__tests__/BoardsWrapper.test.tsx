import { render, screen } from "@/test/test-utils";
import { BoardsWrapper, useBoardsQuery } from "@/features/board";

jest.mock("@/features/board/api");

const useBoardsQueryMock = useBoardsQuery as jest.Mock;

describe("BoardsWrapper fetch states", () => {
  it("displays loading state", () => {
    useBoardsQueryMock.mockReturnValue({ isLoading: true });

    render(<BoardsWrapper />);

    expect(screen.getByText("Loading...")).toBeInTheDocument();
  });

  it("displays error state", () => {
    useBoardsQueryMock.mockReturnValue({ isError: true });

    render(<BoardsWrapper />);

    expect(screen.getByText("Error...")).toBeInTheDocument();
  });

  it("displays board on success", () => {
    useBoardsQueryMock.mockReturnValue({
      isSuccess: true,
    });

    render(<BoardsWrapper />);

    expect(screen.getByText("View and manage boards")).toBeInTheDocument();
  });
});
