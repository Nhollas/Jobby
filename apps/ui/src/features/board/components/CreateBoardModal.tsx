"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  Button,
  Input,
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from "@/components/ui";
import { useForm } from "react-hook-form";
import {
  CreateBoardDTO,
  CreateBoardSchema,
  useCreateBoard,
} from "@/features/board";

export function CreateBoardModal() {
  const { mutateAsync } = useCreateBoard();

  const router = useRouter();

  const [open, setOpen] = useState(false);

  useEffect(() => {
    setOpen(true);
  }, []);

  const form = useForm<CreateBoardDTO>({
    resolver: zodResolver(CreateBoardSchema),
    defaultValues: {
      name: "",
    },
  });

  const { isDirty, isValid, isSubmitted } = form.formState;

  async function onSubmit(values: CreateBoardDTO) {
    await mutateAsync(values);

    setOpen(false);
    router.push("/track/boards");
  }

  return (
    <Dialog open={open} onOpenChange={router.back}>
      <DialogContent className="sm:max-w-md">
        <DialogHeader>
          <DialogTitle>Create Board</DialogTitle>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Name</FormLabel>
                    <FormControl>
                      <Input placeholder="Name" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <div className="flex flex-row gap-x-2">
                <Button
                  type="button"
                  variant="outline"
                  onClick={() => {
                    setOpen(false);
                    router.push("/track/boards");
                  }}
                >
                  Cancel
                </Button>
                <Button type="submit" disabled={!isDirty || isSubmitted}>
                  Submit
                </Button>
              </div>
            </form>
          </Form>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
}
