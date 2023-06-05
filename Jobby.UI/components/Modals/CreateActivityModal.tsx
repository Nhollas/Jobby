"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Modal } from "@/components/Modal";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Form } from "../ui/form";

interface Props {
  boardId: string;
  jobId?: string;
}

const formSchema = z.object({
  title: z.string().nonempty(),
  type: z.number(),
  startDate: z.date().optional(),
  endDate: z.date().optional(),
  jobId: z.string().optional(),
  note: z.string().optional(),
  completed: z.boolean(),
  boardId: z.string(),
});

export const CreateActivityModal = ({ jobId, boardId }: Props) => {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: "",
      type: 0,
      completed: false,
      jobId,
      boardId,
    },
  });

  const router = useRouter();

  async function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values);
  }

  return (
    <Modal>
      <Card className="z-50 h-full w-full max-w-lg overflow-scroll transition-all animate-in fade-in-90 zoom-in-90 slide-in-from-bottom-10 duration-100 sm:slide-in-from-bottom-0">
        <CardHeader>
          <CardTitle>Create Activity</CardTitle>
          <CardDescription>
            Fill out info for your new activity.
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Form {...form}>
            <form
              onSubmit={form.handleSubmit(onSubmit)}
              className="space-y-6"
            ></form>
          </Form>
        </CardContent>
      </Card>
    </Modal>
  );
};
