"use client";

import { Board } from "types";
import { useContext, useEffect, useState } from "react";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import { useRouter } from "next/navigation";
import { postAsync } from "@/lib/clientFetch";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "../ui/input";
import { useAuth } from "@clerk/nextjs";
import { z } from "zod";
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from "@/components/ui/form";
import { useForm } from "react-hook-form";

const formSchema = z.object({
  name: z.string().nonempty({ message: "The Name field is required" }),
});

export const CreateBoardModal = () => {
  const { setBoards } = useContext(BoardsAndJobsContext);
  const { getToken } = useAuth();

  const router = useRouter();

  const [open, setOpen] = useState(false);

  useEffect(() => {
    setOpen(true);
  }, []);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
    },
  });

  async function onSubmit(values: z.infer<typeof formSchema>) {
    const createdBoard = await postAsync<any, Board>("/board/create", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    if (createdBoard) {
      setBoards((prev: Board[]) => [...prev, createdBoard]);
    }

    router.back();
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
                <Button type="button" variant="outline" onClick={router.back}>
                  Cancel
                </Button>
                <Button type="submit">Submit</Button>
              </div>
            </form>
          </Form>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
