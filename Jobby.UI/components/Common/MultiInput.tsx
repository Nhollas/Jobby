import clsx from "clsx";
import { Base } from "types/requests/CreateContactRequest";
import { v4 as uuid } from "uuid";
import { Input } from "@/components/ui/input";
import { Button } from "../ui/button";
import { X } from "lucide-react";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from "../ui/select";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../ui/card";

interface Props {
  list: Base[];
  name: string;
  label: string;
  description: string;
  onChange: (value: Base) => void;
  addItem: (value: Base) => void;
  removeItem: (value: Base) => void;
  placeholder?: string;
  chooseType?: boolean;
  icon?: React.ReactNode;
}

const MultiInput = (props: Props) => {
  const {
    list,
    name,
    label,
    description,
    onChange,
    placeholder,
    addItem,
    removeItem,
    chooseType,
    icon,
  } = props;

  return (
    <Card>
      <CardHeader>
        <CardTitle>{label}</CardTitle>
        <CardDescription>{description}</CardDescription>
      </CardHeader>
      {list.length > 0 && (
        <CardContent className="grid gap-2">
          {list.map((item, index) => (
            <div key={index} className="flex flex-row gap-x-2">
              <div className="flex h-8 items-center justify-center">{icon}</div>
              <Input
                type="text"
                className="h-8 bg-white"
                name={name}
                value={item.value}
                onChange={(e) => onChange({ ...item, value: e.target.value })}
                placeholder={placeholder}
              />

              {chooseType && (
                <Select
                  value={`${item.type}`}
                  onValueChange={(e) => {
                    return onChange({ ...item, type: +e });
                  }}
                >
                  <SelectTrigger className="h-8 w-[200px] truncate">
                    <SelectValue placeholder="Select a type"></SelectValue>
                  </SelectTrigger>
                  <SelectContent className="w-[200px]">
                    <SelectGroup>
                      <SelectLabel>Types</SelectLabel>
                      <SelectItem value="0">Work</SelectItem>
                      <SelectItem value="1">Personal</SelectItem>
                    </SelectGroup>
                  </SelectContent>
                </Select>
              )}
              <Button
                onClick={() => removeItem(item)}
                variant="outline"
                className="h-8 w-8 rounded-full p-0"
              >
                <X className="h-8 w-8 p-2" />
                <span className="sr-only">Remove</span>
              </Button>
            </div>
          ))}
        </CardContent>
      )}
      <CardFooter>
        <Button
          onClick={() =>
            addItem({ value: "", id: uuid(), type: chooseType ? 0 : undefined })
          }
          className={clsx(
            list.length === 0 ? "w-full py-2" : "w-max",
            "flex h-8 py-4 px-6"
          )}
        >
          Add {placeholder}
        </Button>
      </CardFooter>
    </Card>
  );
};

export default MultiInput;
