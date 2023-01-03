const setCheckbox = (value) => (value ? "Checked" : "");

const Input = (props) => {
  const {
    type,
    className,
    onChange,
    placeholder,
    value,
    name,
    checked,
    label,
  } = props;

  let formattedClassName = className
    ? className
    : "px-3 py-1 border border-gray-300";

  if (type === "textarea") {
    return (
      <textarea
        type={type}
        className={formattedClassName}
        onChange={onChange}
        placeholder={placeholder}
        value={value}
        name={name}
      ></textarea>
    );
  } else {
    if (label !== undefined) {
      return (
        <div className='flex flex-col justify-center gap-y-1.5'>
          {label !== undefined && (
            <label className='text-sm font-medium' for={name}>
              {label}
            </label>
          )}
          <input
            type={type}
            className={formattedClassName}
            onChange={onChange}
            placeholder={placeholder}
            value={value}
            name={name}
            checked={setCheckbox(checked)}
          ></input>
        </div>
      );
    } else {
      return (
        <input
          type={type}
          className={formattedClassName}
          onChange={onChange}
          placeholder={placeholder}
          value={value}
          name={name}
          checked={setCheckbox(checked)}
        ></input>
      );
    }
  }
};

export default Input;
