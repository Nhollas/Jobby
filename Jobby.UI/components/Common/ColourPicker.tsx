import { useEffect, useRef, useState } from "react";

interface Props {
  onChange: (value: any) => void;
}

const defaultColours = [
  "#ffffff",
  "#ef4444",
  "#f97316",
  "#84cc16",
  "#10b981",
  "#0ea5e9",
  "#8b5cf6",
  "#d946ef",
  "#f43f5e",
];

const ColourPicker = (props: Props) => {
  const { onChange } = props;
  const [selectedColour, setSelectedColour] = useState(defaultColours[0]);
  const [showPicker, setShowPicker] = useState(false);
  const pickerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const handleClickOutside = (event: any) => {
    if (pickerRef.current && !pickerRef.current.contains(event.target)) {
      setShowPicker(false);
    }
  };

  return (
    <div
      className="flex flex-col gap-y-4"
      ref={pickerRef}
      onClick={() => setShowPicker((prev) => !prev)}
    >
      <div className="flex flex-col justify-center gap-y-1.5">
        <label className="text-sm font-medium" htmlFor="colour">
          Colour
        </label>
        <div className="relative h-10 w-20">
          <input
            type="text"
            disabled
            className="h-10 w-full cursor-pointer rounded-md border border-input bg-gray-50 p-1 px-3 py-1"
            name="colour"
          ></input>
          <div
            className="absolute inset-1.5 rounded-md"
            style={{
              backgroundColor: selectedColour,
            }}
          ></div>
        </div>
      </div>
      {showPicker && (
        <div className="flex w-max flex-wrap gap-1 bg-white p-1.5 outline outline-1 outline-gray-300">
          {defaultColours.map((colour, index) => (
            <button
              type="button"
              key={index}
              className="h-8 w-8 rounded-md"
              style={{ backgroundColor: colour }}
              onClick={() => {
                setSelectedColour(colour);
                onChange(colour);
              }}
            ></button>
          ))}
        </div>
      )}
    </div>
  );
};

export default ColourPicker;
