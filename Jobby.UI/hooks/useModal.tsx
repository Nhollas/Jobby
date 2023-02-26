import { useState } from "react";

const useModal = () => {
  const [isVisible, setIsVisible] = useState(false);
  const [modalContent, setModalContent] = useState(<></>);

  const closeModal = () => {
    setIsVisible((prev) => !prev);
    setModalContent(null);
  };
  const handleModal = (content: JSX.Element) => {
    setModalContent(content);
    setIsVisible(true);
  };

  return { isVisible, closeModal, handleModal, modalContent };
};

export default useModal;
