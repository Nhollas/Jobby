"use client";

import { ReactNode, useEffect } from "react";

export const Modal = ({ children }: { children: ReactNode }) => {
  useEffect(() => {
    // Add overflow hidden to the body element
    document.body.style.overflow = "hidden";

    // Remove overflow hidden when the Modal component unmounts
    return () => {
      document.body.style.overflow = "auto";
    };
  }, []);

  return (
    <div className="fixed inset-0 z-50 flex h-screen items-start justify-center bg-background/80 backdrop-blur-sm sm:items-center">
      {children}
    </div>
  );
};
