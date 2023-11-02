import { render, screen } from "@testing-library/react";
import { Boards, useBoardsQuery } from "@/features/board";
import "@testing-library/jest-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

jest.mock("@/features/board/api");

const queryClient = new QueryClient();

describe("Boards", () => {
  it("displays loading state", () => {
    (useBoardsQuery as jest.Mock).mockReturnValue({ isLoading: true });

    render(
      <QueryClientProvider client={queryClient}>
        <Boards />
      </QueryClientProvider>
    );

    expect(screen.getByText("Loading...")).toBeInTheDocument();
  });

  it("displays error state", () => {
    (useBoardsQuery as jest.Mock).mockReturnValue({ isError: true });

    render(
      <QueryClientProvider client={queryClient}>
        <Boards />
      </QueryClientProvider>
    );

    expect(screen.getByText("Error")).toBeInTheDocument();
  });

  it("displays board on success", () => {
    (useBoardsQuery as jest.Mock).mockReturnValue({
      isSuccess: true,
    });

    render(
      <QueryClientProvider client={queryClient}>
        <Boards />
      </QueryClientProvider>
    );

    expect(screen.getByText("View and manage boards")).toBeInTheDocument();
  });
});
