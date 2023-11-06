import {
  render,
  screen,
  fireEvent,
  waitFor,
  act,
} from "@testing-library/react";
import { useDeleteBoard, DeleteBoardModal } from "@/features/board";
import { useRouter } from "next/navigation";

jest.mock("@/features/board/api/deleteBoard", () => ({
  ...jest.requireActual("@/features/board/api/deleteBoard"),
  useDeleteBoard: jest.fn(),
}));

jest.mock("next/navigation", () => ({
  useRouter: jest.fn(),
}));

const mockedUseDeleteBoard = useDeleteBoard as jest.Mock;
const mockedUseRouter = useRouter as jest.Mock;

const mockedBoardRef = "mockedBoardRef";

describe("DeleteBoardModal", () => {
  const mutateAsyncMock = jest.fn();
  const pushMock = jest.fn();

  beforeEach(() => {
    mockedUseDeleteBoard.mockReturnValue({
      mutateAsync: mutateAsyncMock,
    });
    mockedUseRouter.mockReturnValue({
      push: pushMock,
    });
  });

  afterEach(() => {
    jest.clearAllMocks();
  });

  describe("Rendering", () => {
    test("should render the modal on mount", async () => {
      render(<DeleteBoardModal boardRef={mockedBoardRef} />);

      await waitFor(() => {
        expect(screen.getByText("Delete Board")).toBeInTheDocument();
      });
    });
  });

  describe("Form Submission", () => {
    test("should call mutateAsync and redirect on form submission", async () => {
      render(<DeleteBoardModal boardRef={mockedBoardRef} />);

      act(() => {
        fireEvent.click(screen.getByRole("button", { name: "Confirm" }));
      });

      await waitFor(() => {
        expect(mutateAsyncMock).toHaveBeenCalled();
        expect(pushMock).toHaveBeenCalledWith("/track/boards");
      });
    });
  });

  describe("Cancel Button", () => {
    beforeEach(() => {
      render(<DeleteBoardModal boardRef={mockedBoardRef} />);
    });

    test("should redirect on cancel button click", async () => {
      act(() => {
        fireEvent.click(screen.getByText("Cancel"));
      });

      await waitFor(() => {
        expect(pushMock).toHaveBeenCalledWith("/track/boards");
      });
    });
  });
});
