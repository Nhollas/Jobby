import { useEffect, useRef, useState } from "react";

interface Props {
  onChange?: (value) => void;
}

const defaultColours = ["#ffffff", "#FF3300", "#FF8162", "#FEC405", "#6A4FEB"];

const ColourPicker = (props: Props) => {
  const { onChange } = props;
  const [selectedColour, setSelectedColour] = useState(defaultColours[0]);
  const [showPicker, setShowPicker] = useState(false);
  const pickerRef = useRef(null);

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const handleClickOutside = (event) => {
    if (pickerRef.current && !pickerRef.current.contains(event.target)) {
      setShowPicker(false);
    }
  };

  return (
    <div className='relative flex'>
      <div ref={pickerRef} onClick={() => setShowPicker((prev) => !prev)}>
        <div className='flex flex-col justify-center gap-y-1.5'>
          <label className='text-sm font-medium' htmlFor='colour'>
            Colour
          </label>
          <input
            style={{ backgroundColor: selectedColour }}
            type='text'
            disabled
            className='max-w-[7rem] rounded-sm border border-gray-300 bg-gray-50 px-3 py-1'
            name='colour'
          ></input>
        </div>
        {showPicker && (
          <div className='absolute -bottom-10 flex flex-wrap gap-x-1 border border-gray-300 bg-white p-1'>
            {defaultColours.map((colour, index) => (
              <button
                type='button'
                key={index}
                className='h-6 w-6 rounded-md'
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
    </div>
  );
};

export default ColourPicker;
