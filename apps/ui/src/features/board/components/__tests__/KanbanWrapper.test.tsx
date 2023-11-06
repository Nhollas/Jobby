import { render, screen } from "@testing-library/react";
import { KanbanWrapper, useBoardQuery } from "@/features/board";
import "@testing-library/jest-dom";
import { boardGenerator } from "@/test/data-generators";

jest.mock("@/features/board/api");

const useBoardQueryMock = useBoardQuery as jest.Mock;

describe("KanbanWrapper fetch states", () => {
  it("displays loading state", () => {
    useBoardQueryMock.mockReturnValue({ isLoading: true });

    render(<KanbanWrapper boardRef="boardRef" />);

    expect(screen.getByText("Loading...")).toBeInTheDocument();
  });

  it("displays error state", () => {
    useBoardQueryMock.mockReturnValue({ isError: true });

    render(<KanbanWrapper boardRef="boardRef" />);

    expect(screen.getByText("Error...")).toBeInTheDocument();
  });

  it("displays board on success", () => {
    const fakeBoard = boardGenerator();

    useBoardQueryMock.mockReturnValue({
      isSuccess: true,
      data: fakeBoard,
    });

    render(<KanbanWrapper boardRef="boardRef" />);

    expect(screen.getByText(fakeBoard.lists[0].name)).toBeInTheDocument();
  });
});
