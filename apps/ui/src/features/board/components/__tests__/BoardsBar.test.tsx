import { BoardsBar, useBoardsQuery } from "@/features/board";
import { boardGenerator } from "@/test/data-generators";
import { render, screen } from "@/test/test-utils";

jest.mock("@/features/board/api");

const useBoardsQueryMock = useBoardsQuery as jest.Mock;

describe("BoardsBar", () => {
  it("displays each board with their name and link to their page", () => {
    const fakeBoards = Array.from({ length: 5 }, () => boardGenerator());

    useBoardsQueryMock.mockReturnValue({
      isSuccess: true,
      data: fakeBoards,
    });

    render(<BoardsBar />);

    fakeBoards.map((board) => {
      expect(screen.getByText(board.name)).toBeInTheDocument();

      const link = screen.getByRole("link", { name: board.name });

      expect(link).toHaveAttribute("href", `/track/board/${board.reference}`);
    });
  });
});
