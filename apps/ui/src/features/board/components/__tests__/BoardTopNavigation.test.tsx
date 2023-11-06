import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import { usePathname } from "next/navigation";
import { BoardTopNavigation } from "@/features/board";
import "@testing-library/jest-dom";

jest.mock("next/navigation", () => ({
  usePathname: jest.fn(),
}));

const mockUsePathname = usePathname as jest.MockedFunction<typeof usePathname>;

describe("BoardTopNavigation", () => {
  test("renders without crashing", () => {
    render(<BoardTopNavigation />);
    const navItems = screen.getAllByRole("link");
    expect(navItems).toHaveLength(3);
  });

  test("renders correct navigation items", () => {
    render(<BoardTopNavigation />);
    const boardLink = screen.getByText("Board");
    const activitiesLink = screen.getByText("Activities");
    const contactsLink = screen.getByText("Contacts");

    expect(boardLink).toBeInTheDocument();
    expect(activitiesLink).toBeInTheDocument();
    expect(contactsLink).toBeInTheDocument();
  });
});

type SelectedLinkScenario = {
  pathname: string;
  selectedLink: string;
  nonSelectedLinks: string[];
};

const selectedLinkScenarios: SelectedLinkScenario[] = [
  {
    pathname: "/track/board/bo_u9imGJVO2V",
    selectedLink: "Board",
    nonSelectedLinks: ["Activities", "Contacts"],
  },
  {
    pathname: "/track/board/bo_u9imGJVO2V/activities/all",
    selectedLink: "Activities",
    nonSelectedLinks: ["Board", "Contacts"],
  },
  {
    pathname: "/track/board/bo_u9imGJVO2V/contacts",
    selectedLink: "Contacts",
    nonSelectedLinks: ["Board", "Activities"],
  },
  {
    pathname: "/track/board/bo_u9imGJVO2V/job/jo_3Cjpv2MUWL/info",
    selectedLink: "Board",
    nonSelectedLinks: ["Activities", "Contacts"],
  },
];

describe.each(selectedLinkScenarios)(
  "BoardTopNavigation",
  ({ pathname, selectedLink, nonSelectedLinks }) => {
    test("the correct link is selected when on board page", () => {
      mockUsePathname.mockReturnValue(pathname);
      render(<BoardTopNavigation />);

      const selectedClassNames = "bg-white font-medium text-primary";

      const activeLink = screen.getByText(selectedLink);

      expect(activeLink).toHaveClass(selectedClassNames);

      nonSelectedLinks.map((link) => {
        const inactiveLink = screen.getByText(link);
        expect(inactiveLink).not.toHaveClass(selectedClassNames);
      });
    });
  }
);
