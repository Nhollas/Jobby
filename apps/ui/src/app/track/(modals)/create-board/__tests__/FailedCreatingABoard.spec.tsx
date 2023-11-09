import { fireEvent, screen, waitFor } from "@/test/test-utils";
import { useRouter } from "next/navigation";
import CreateBoardModalPage from "../page";
import { renderWithProviders } from "@/test/custom-render";
import { server } from "@/test/server/browser";
import { HttpResponse, http } from "msw";

jest.mock("next/navigation", () => ({
  useRouter: jest.fn(),
}));

const mockedUseRouter = useRouter as jest.Mock;

describe("Failed Creating A Board Modal Page", () => {
  it("should display error message", async () => {
    const mockedMutateAsync = jest.fn();

    mockedUseRouter.mockReturnValue({
      push: jest.fn(),
    });

    server.use(
      http.post("/api/board", () => {
        return new HttpResponse("", { status: 500 });
      })
    );

    renderWithProviders(<CreateBoardModalPage />);

    fireEvent.input(screen.getByLabelText("Name"), {
      target: { value: "My Board" },
    });

    fireEvent.submit(screen.getByText("Submit"));

    waitFor(() => {
      expect(mockedMutateAsync).toHaveBeenCalledWith({
        name: "My Board",
      });

      expect(mockedUseRouter().push).toHaveBeenCalledWith("/track/boards");

      expect(
        screen.getByText("There was an error creating your board")
      ).toBeInTheDocument();
    });
  });
});
