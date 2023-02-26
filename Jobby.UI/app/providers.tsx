"use client";

import { SessionProvider } from "next-auth/react";
import { ModalProvider } from "../contexts/ModalContext";

export default function Providers({ children }) {
  return (
    <SessionProvider>
      <ModalProvider>{children}</ModalProvider>
    </SessionProvider>
  );
}
