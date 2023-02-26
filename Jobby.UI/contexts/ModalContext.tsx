import useModal from "../hooks/useModal";
import Modal from "../components/Common/Modal";
import { createContext } from "react";

type ModalContextType = {
  handleModal: (content: JSX.Element) => void;
  isVisible: boolean;
  modalContent: JSX.Element;
  closeModal: () => void;
};

const ModalContext = createContext<ModalContextType>({
  isVisible: false,
  handleModal: (content: JSX.Element) => {},
  modalContent: <></>,
  closeModal: () => {},
});

const ModalProvider = ({ children }) => {
  const { isVisible, handleModal, modalContent, closeModal } = useModal();
  return (
    <ModalContext.Provider
      value={{ isVisible, handleModal, modalContent, closeModal }}
    >
      <Modal />
      {children}
    </ModalContext.Provider>
  );
};

export { ModalContext, ModalProvider };
