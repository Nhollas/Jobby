import { render, screen, fireEvent, waitFor, act } from "@/test/test-utils";
import { useCreateBoard, CreateBoardModal } from "@/features/board";
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

describe("CreateBoardModal", () => {
  const mutateAsyncMock = jest.fn();
  const pushMock = jest.fn();

  beforeEach(() => {
    mockedUseCreateBoard.mockReturnValue({
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
      render(<CreateBoardModal />);

      await waitFor(() => {
        expect(screen.getByText("Create Board")).toBeInTheDocument();
      });
    });

    test("should render form fields", async () => {
      render(<CreateBoardModal />);

      await waitFor(() => {
        expect(screen.getByPlaceholderText("Name")).toBeInTheDocument();
      });
    });
  });

  describe("Form Submission", () => {
    beforeEach(() => {
      render(<CreateBoardModal />);
    });

    test("should call mutateAsync and redirect on form submission", async () => {
      act(() => {
        fireEvent.input(screen.getByPlaceholderText("Name"), {
          target: { value: "Test Board" },
        });

        fireEvent.submit(screen.getByText("Submit"));
      });

      await waitFor(() => {
        expect(mutateAsyncMock).toHaveBeenCalled();
        expect(pushMock).toHaveBeenCalledWith("/track/boards");
      });
    });
  });

  describe("Cancel Button", () => {
    beforeEach(() => {
      render(<CreateBoardModal />);
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
