"use client";

import React, { useContext } from "react";
import { ModalContext } from "../../contexts/ModalContext";

const Modal = () => {
  let { modalContent, isVisible } = useContext(ModalContext);

  if (isVisible) {
    return modalContent;
  }
};

export default Modal;
