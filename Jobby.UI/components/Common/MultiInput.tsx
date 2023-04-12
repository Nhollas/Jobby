import clsx from "clsx";
import { Base } from "types/requests/CreateContactRequest";
import { v4 as uuid } from "uuid";

interface Props {
  list: Base[];
  name: string;
  label: string;
  onChange: (value: Base) => void;
  addItem: (value: Base) => void;
  removeItem: (value: Base) => void;
  placeholder?: string;
  chooseType?: boolean;
}

const MultiInput = (props: Props) => {
  const { list, name, label, onChange, placeholder, addItem, removeItem, chooseType } =
    props;

  return (
    <div className="flex w-full flex-col justify-center gap-y-1.5">
      <label className="text-sm font-medium" htmlFor={name}>
        {label}
      </label>
      <div
      className={clsx(list.length === 0 ? "p-1" : "p-3", "border border-gray-300 bg-gray-50 gap-y-3 flex flex-col")}>
        {list.map((item, index) => (
          <div key={index} className="flex flex-row gap-x-2">
            <input
              className="w-full bg-gray-50 px-2 text-sm"
              placeholder={placeholder}
              onChange={(e) => onChange({ ...item, value: e.target.value })}
              value={item.value}
              name={name}
            />
            {chooseType && (
              <select
                className="w-max bg-gray-50 px-2 text-xs border border-gray-300 rounded-lg"
                onChange={(e) =>
                  onChange({ ...item, type: e.target.value as Base["type"] })
                }
                value={item.type}
                name={name}
              >
                <option value="work">Work</option>
                <option value="home">Home</option>
              </select>
            )}
            <button
              type="button"
              onClick={() => removeItem(item)}
              className="rounded-lg p-1 text-sm hover:bg-gray-100"
            >
              <i className="bi-x text-xl text-slate-900"></i>
            </button>
          </div>
        ))}
        <button
            type="button"
          onClick={() => addItem({ value: "", id: uuid()})}
          className={clsx(list.length === 0 ? "w-full py-2" : "w-max", "px-6 rounded-md bg-blue-100 p-1 text-xs")}
        >
          <span>Add {placeholder}</span>
        </button>
      </div>
    </div>
  );
};

export default MultiInput;
