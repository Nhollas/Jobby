type List = {
    value: string;
    label: string;
  };
  
  interface Props {
    list: List[];
    name: string;
    label?: string;
    onChange: (value: any) => void;
  }
  
  const MultiInput = (props: Props) => {
    const { list, name, label, onChange } = props;
  
    return (
      <div className='flex w-full flex-col justify-center gap-y-1.5'>
        <label className='text-sm font-medium' htmlFor={name}>
          {label}
        </label>
        <select
          className='border border-gray-300 bg-gray-50 px-3 py-1'
          name={name}
          onChange={(e) => onChange(e.target.value)}
        >
          {options.map((option, i) => (
            <option key={i} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>

      </div>
    );
  };
  
  export default MultiInput;
  