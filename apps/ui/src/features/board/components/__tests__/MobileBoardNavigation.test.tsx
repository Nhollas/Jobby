import { fireEvent, render, screen } from "@testing-library/react";
import { usePathname } from "next/navigation";
import { MobileBoardNavigation, useBoardsQuery } from "@/features/board";
import "@testing-library/jest-dom";

jest.mock("next/navigation", () => ({
  usePathname: jest.fn(),
  useRouter: jest.fn(),
}));

jest.mock("@/features/board/api");
jest.mock("@clerk/nextjs", () => ({
  useAuth: jest.fn().mockReturnValue({
    signOut: jest.fn(),
  }),
}));

const useBoardsQueryMock = useBoardsQuery as jest.Mock;

const mockUsePathname = usePathname as jest.MockedFunction<typeof usePathname>;

const requiredLinkScenarios = [
  "/track/boards",
  "/track/contacts",
  "/track/contact",
];

describe.each(requiredLinkScenarios)(
  "MobileBoardNavigation - Required Link Scenario: %s",
  (requiredLink) => {
    test("should show the CTA when the link is required", () => {
      mockUsePathname.mockReturnValue(requiredLink);
      useBoardsQueryMock.mockReturnValue({
        isSuccess: true,
      });

      render(<MobileBoardNavigation />);
      const ctaButton = screen.getByRole("button");
      fireEvent.click(ctaButton);

      expect(screen.getByText("Track")).toBeInTheDocument();
    });
  }
);
