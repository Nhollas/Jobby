"use client";

import clsx from "clsx";
import { X } from "lucide-react";
import { Control, useFieldArray } from "react-hook-form";
import { Button } from "./button";
import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./form";
import { Input } from "./input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "./select";

type MultiInputProps = {
  control: Control<any>;
  name: string;
  label: string;
  description: string;
  icon?: React.ReactNode;
  placeholder?: string;
  propertyName?: string;
  includeType?: boolean;
};

export function MultiInput({
  control,
  name,
  placeholder,
  label,
  description,
  icon,
  propertyName,
  includeType,
}: MultiInputProps) {
  const { fields, append, remove } = useFieldArray({
    name,
    control,
  });

  return (
    <div>
      <FormLabel>{label}</FormLabel>
      <FormDescription>{description}</FormDescription>
      {fields.length > 0 && (
        <div className="flex flex-col gap-2 py-4">
          {fields.map((field, index) => (
            <FormField
              key={field.id}
              control={control}
              name={`${name}.${index}${propertyName ? `.${propertyName}` : ""}`}
              render={({ field }) => (
                <FormItem>
                  <FormControl>
                    <div className="flex flex-row items-center gap-x-2 rounded-lg border px-4 py-1">
                      {icon}
                      <Input placeholder={placeholder} {...field} />
                      {includeType && (
                        <FormField
                          control={control}
                          name={`${name}.${index}.type`}
                          render={({ field }) => (
                            <FormItem>
                              <Select
                                onValueChange={field.onChange}
                                defaultValue={field.value}
                              >
                                <FormControl>
                                  <SelectTrigger>
                                    <SelectValue placeholder="Select a type" />
                                  </SelectTrigger>
                                </FormControl>
                                <SelectContent>
                                  <SelectItem value="0">Work</SelectItem>
                                  <SelectItem value="1">Personal</SelectItem>
                                </SelectContent>
                              </Select>
                            </FormItem>
                          )}
                        />
                      )}
                      <Button
                        onClick={() => remove(index)}
                        variant="outline"
                        type="button"
                        className="h-8 w-8 rounded-full"
                      >
                        <X className="h-5 w-5 flex-shrink-0" />
                        <span className="sr-only">Remove</span>
                      </Button>
                    </div>
                  </FormControl>
                  <FormMessage className="text-xs" />
                </FormItem>
              )}
            />
          ))}
        </div>
      )}
      <Button
        type="button"
        onClick={() =>
          append(
            propertyName
              ? { [propertyName]: "", ...(includeType && { type: "0" }) }
              : ""
          )
        }
        className={clsx(
          fields.length === 0 ? "!mt-4 w-full py-2" : "w-max",
          "flex h-8 py-4 px-6"
        )}
      >
        Add
      </Button>
    </div>
  );
}

export default MultiInput;
