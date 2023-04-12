import { createContext, Dispatch, SetStateAction } from "react";
import { Board } from "types";

type ModalContextType = {
  boards: Board[];
  setBoards: Dispatch<SetStateAction<Board[]>>
};

const BoardsAndJobsContext = createContext<ModalContextType>({
  boards: [],
  setBoards: () => {},
});

export default BoardsAndJobsContext;
