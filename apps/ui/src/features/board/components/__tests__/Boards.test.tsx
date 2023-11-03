import { render, screen } from "@testing-library/react";
import { Boards, useBoardsQuery } from "@/features/board";
import "@testing-library/jest-dom";

jest.mock("@/features/board/api");

describe("Boards", () => {
  it("displays loading state", () => {
    (useBoardsQuery as jest.Mock).mockReturnValue({ isLoading: true });

    render(<Boards />);

    expect(screen.getByText("Loading...")).toBeInTheDocument();
  });

  it("displays error state", () => {
    (useBoardsQuery as jest.Mock).mockReturnValue({ isError: true });

    render(<Boards />);

    expect(screen.getByText("Error")).toBeInTheDocument();
  });

  it("displays board on success", () => {
    (useBoardsQuery as jest.Mock).mockReturnValue({
      isSuccess: true,
    });

    render(<Boards />);

    expect(screen.getByText("View and manage boards")).toBeInTheDocument();
  });
});
