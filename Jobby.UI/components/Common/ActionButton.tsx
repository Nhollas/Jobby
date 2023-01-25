interface Props {
  type?: "button" | "submit" | "reset";
  text?: string;
  onClick?(): void;
  variant?: "primary" | "secondary" | "danger";
  rounded?: boolean;
  extended?: boolean;
  className?: string;
}

export const ActionButton = (props: Props) => {
  const { type, text, onClick, variant, rounded, extended, className } = props;

  const classNameMap = {
    primary:
      "border bg-main-blue py-2 px-8 text-base font-medium text-white hover:border-main-blue hover:bg-gray-50 hover:text-black",
    secondary: "border border-gray-300 bg-white py-2 px-4 font-medium",
    danger: "rounded-lg bg-main-red px-4 py-2 font-medium text-white",
    default: "default-button",
  };

  const generateClassName = (rounded, extended, variant, className) => {
    let result = classNameMap[variant] || classNameMap.default;

    if (rounded) {
      result += " rounded-full";
    }
    if (extended) {
      result += " w-full";
    } else {
      result += " w-max";
    }

    if (className) {
      result += ` ${className}`;
    }

    return result;
  };

  const generatedClassName = generateClassName(
    rounded,
    extended,
    variant,
    className
  );

  return (
    <button type={type} className={generatedClassName} onClick={onClick}>
      {text}
    </button>
  );
};

export default ActionButton;
