"use client";

import React, { useContext } from "react";
import { ModalContext } from "../../contexts/ModalContext";
import ModalContainer from "./ModalContainer";

const Modal = () => {
  let { modalContent, isVisible } = useContext(ModalContext);

  if (isVisible) {
    return <ModalContainer>{modalContent}</ModalContainer>;
  }
};

export default Modal;
