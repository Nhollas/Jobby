import { createContext, Dispatch, SetStateAction } from "react";
import { Job } from "../types";

export const ModalContext = createContext<{
  showViewJobModal: {
    visible: boolean;
    job: Job | null;
  };
  setShowViewJobModal: Dispatch<
    SetStateAction<{
      visible: boolean;
      job: Job | null;
    }>
  >;
}>({
  showViewJobModal: {
    visible: false,
    job: null,
  },
  setShowViewJobModal: () => {},
});
