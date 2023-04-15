import { createContext } from "react";

type ModalContextType = {
  isOpen: boolean;
  toggleOpen: () => void;
};

const NavigationContext = createContext<ModalContextType>({
  isOpen: false,
  toggleOpen: () => {},
});

export default NavigationContext;
