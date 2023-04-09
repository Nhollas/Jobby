import { Kanban } from "components/Board";
import { JobListPreview } from "types";

const fakeLists: JobListPreview[] = [
  {
    name: "Applied",
    jobs: [
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 0,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "99c93044-ce6d-8537-3962-3a0a36a48a36",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 1,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "39a149ff-fef5-87cf-81c8-3a0a36a49433",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 2,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "93331693-5ef7-f231-cf34-3a0a36a49e76",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 3,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "9a2c3ac7-2638-f13d-e828-3a0a36a4a8e0",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
    ],
    boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
    id: "50083ebc-db15-849d-f6f4-3a0a345c3349",
    createdDate: "0001-01-01T00:00:00",
    lastUpdated: "0001-01-01T00:00:00",
  },
  {
    name: "Wishlist",
    jobs: [
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 2,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "93331693-5ef7-f231-cf34-3a0a36a49e76",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 3,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "9a2c3ac7-2638-f13d-e828-3a0a36a4a8e0",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
    ],
    boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
    id: "6c8bfce9-de3d-377d-cab0-3a0a345c334d",
    createdDate: "0001-01-01T00:00:00",
    lastUpdated: "0001-01-01T00:00:00",
  },
  {
    name: "Interview",
    jobs: [
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 1,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "39a149ff-fef5-87cf-81c8-3a0a36a49433",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 2,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "93331693-5ef7-f231-cf34-3a0a36a49e76",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 3,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "9a2c3ac7-2638-f13d-e828-3a0a36a4a8e0",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
    ],
    boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
    id: "01c19aa9-9c5a-472e-f3aa-3a0a345c334d",
    createdDate: "0001-01-01T00:00:00",
    lastUpdated: "0001-01-01T00:00:00",
  },
  {
    name: "Offer",
    jobs: [
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 1,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "9a2c3ac7-2638-f13d-e828-3a0a36a4a8e0",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
    ],
    boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
    id: "7c7fff48-20d4-f0f8-4b04-3a0a345c334d",
    createdDate: "0001-01-01T00:00:00",
    lastUpdated: "0001-01-01T00:00:00",
  },
  {
    name: "Rejected",
    jobs: [
      {
        company: "Amazing Job",
        title: "Amazing Job",
        index: 1,
        colour: "#ffffff",
        jobListId: "50083ebc-db15-849d-f6f4-3a0a345c3349",
        boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
        id: "9a2c3ac7-2638-f13d-e828-3a0a36a4a8e0",
        createdDate: "0001-01-01T00:00:00",
        lastUpdated: "0001-01-01T00:00:00",
      },
    ],
    boardId: "4fabd524-b129-149f-0278-3a0a345c3349",
    id: "93ea19ca-2b4c-9c7c-4e75-3a0a345c334d",
    createdDate: "0001-01-01T00:00:00",
    lastUpdated: "0001-01-01T00:00:00",
  },
];

export default function Loading() {
  // You can add any UI inside Loading, including a Skeleton.
  return (
    <Kanban
      boardId={"4fabd524-b129-149f-0278-3a0a345c3349"}
      lists={fakeLists}
      boardsDictionary={[]}
      loading
    />
  );
}
