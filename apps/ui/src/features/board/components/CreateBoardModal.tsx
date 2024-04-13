"use client";

import { useEffect, useState } from "react";
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
import { useRouter } from "next/navigation";
import { RotateCw, Save } from "lucide-react";

export function CreateBoardModal() {
  const { mutateAsync } = useCreateBoard();
  const [open, setOpen] = useState(false);
  const router = useRouter();

  useEffect(() => {
    setOpen(true);
  }, []);

  const form = useForm<CreateBoardDTO>({
    resolver: zodResolver(CreateBoardSchema),
    defaultValues: {
      name: "",
    },
  });

  const { isDirty, isSubmitted, isSubmitting } = form.formState;

  async function onSubmit(values: CreateBoardDTO) {
    try {
      await mutateAsync(values);
    } catch (error) {}

    setOpen(false);
    router.back();
  }

  return (
    <Dialog open={open} onOpenChange={() => router.back()}>
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
                    router.back();
                  }}
                >
                  Cancel
                </Button>
                <Button
                  type="submit"
                  className="items-center gap-x-2"
                  disabled={!isDirty || isSubmitted}
                >
                  Submit
                  {isSubmitting ? (
                    <RotateCw className="h-5 w-5 flex-shrink-0 animate-spin" />
                  ) : (
                    <Save className="h-5 w-5 flex-shrink-0" />
                  )}
                </Button>
              </div>
            </form>
          </Form>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
}
