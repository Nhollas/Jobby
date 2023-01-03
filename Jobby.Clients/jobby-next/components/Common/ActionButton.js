const ActionButton = (props) => {
  const { type, text, onClick, variant, rounded, extended } = props;

  const classNameMap = {
    primary:
      "border bg-main-blue py-2 px-8 text-base font-medium text-white hover:border-main-blue hover:bg-gray-50 hover:text-black",
    secondary: "border border-gray-300 bg-white py-2 px-4 font-medium",
    danger: "rounded-lg bg-main-red px-4 py-2 font-medium text-white",
    default: "default-button",
  };

  const generateClassName = (rounded, extended, variant) => {
    let className = classNameMap[variant] || classNameMap.default;

    if (rounded) {
      className += " rounded-full";
    }
    if (extended) {
      className += " w-full";
    } else {
      className += " w-max";
    }

    return className;
  };

  const className = generateClassName(rounded, extended, variant);

  return (
    <button type={type} className={className} onClick={onClick}>
      {text}
    </button>
  );
};

export default ActionButton;
