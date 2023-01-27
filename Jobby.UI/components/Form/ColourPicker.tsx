import Input from "./Input";

interface Props {
  type: string;
  className?: string;
  onChange?: (value) => void;
  placeholder?: string;
  name: string;
  value?: string | number;
  checked?: boolean;
  label?: string;
  hidden?: boolean;
}

const defaultColours = ["#ffffff"];

const ColourPicker = (props: Props) => {
  return <Input name="colour" type="hidden" value={va} />;
};

export default ColourPicker;
