import clsx from "clsx";
import { X } from "lucide-react";
import { Control, FieldErrors, useFieldArray } from "react-hook-form";
import { Button } from "../ui/button";
import { FormMessage } from "../ui/form";
import { Input } from "../ui/input";

type MultiInputProps = {
  control: Control<any>;
  name: string;
  label: string;
  description: string;
  icon?: React.ReactNode;
  register: any;
  placeholder?: string;
  errors: FieldErrors<any>;
};

function MultiInput({
  control,
  name,
  icon,
  register,
  placeholder,
  errors,
}: MultiInputProps) {
  const { fields, append, remove } = useFieldArray({
    name,
    control,
  });

  console.log(fields);

  return (
    <div className="!m-0 grid gap-2">
      {fields.map((field, index) => {
        const errorForField = errors?.[name]?.[index]?.value;

        console.log(errorForField);

        return (
          <div
            key={field.id}
            className="flex flex-row items-center gap-x-2 rounded-lg border px-4 py-1"
          >
            {icon}
            <Input
              placeholder={placeholder}
              className="h-6 border-0 py-1"
              {...register(`${name}.${index}.value` as const)}
            />
            <Button
              onClick={() => remove(index)}
              variant="outline"
              type="button"
              className="h-8 w-8 rounded-full"
            >
              <X className="h-5 w-5 flex-shrink-0" />
              <span className="sr-only">Remove</span>
            </Button>
            {errorForField && (
              <FormMessage className="text-xs">
                {errorForField.message}
              </FormMessage>
            )}
          </div>
        );
      })}
      <Button
        type="button"
        onClick={() => append({ value: "" })}
        className={clsx(
          fields.length === 0 ? "!mt-4 w-full py-2" : "w-max",
          "flex h-8 py-4 px-6"
        )}
      >
        Add
      </Button>
      <FormMessage className="text-xs" />
    </div>
  );
}

export default MultiInput;
