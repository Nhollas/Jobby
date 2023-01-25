interface Props {
  type: string;
  className: string;
  onChange?: (value) => void;
  placeholder?: string;
  name: string;
  value: string | number;
  checked?: boolean;
  label: string;
}

const Input = (props: Props) => {
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
            <label className='text-sm font-medium' htmlFor={name}>
              {label}
            </label>
          )}
          <input
            type={type}
            className={formattedClassName}
            onChange={onChange}
            readOnly={onChange ? false : true}
            disabled={onChange ? false : true}
            placeholder={placeholder}
            value={value}
            name={name}
            checked={checked}
          ></input>
        </div>
      );
    } else {
      return (
        <input
          type={type}
          className={formattedClassName}
          onChange={onChange}
          readOnly={onChange ? false : true}
          disabled={onChange ? false : true}
          placeholder={placeholder}
          value={value}
          name={name}
          checked={checked}
        ></input>
      );
    }
  }
};

export default Input;
