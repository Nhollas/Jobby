"use client";

import { Board } from "types";
import { useContext, useState } from "react";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import { useRouter } from "next/navigation";
import { postAsync } from "@/lib/clientFetch";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "../ui/input";
import { Label } from "../ui/label";
import { useAuth } from "@clerk/nextjs";

export const CreateBoardModal = () => {
  const { setBoards } = useContext(BoardsAndJobsContext);
  const { getToken } = useAuth();

  const router = useRouter();
  const [name, setName] = useState("");

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    var board = {
      name,
    };

    const createdBoard = await postAsync<any, Board>("/board/create", board, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    console.log("createdBoard", createdBoard);

    setBoards((prev: Board[]) => [...prev, createdBoard]);

    router.back();
  };

  return (
    <Dialog defaultOpen onOpenChange={router.back}>
      <DialogContent className="sm:max-w-md">
        <DialogHeader>
          <DialogTitle>Create Board</DialogTitle>
          <DialogDescription>
            Are you sure you want to delete this board?
          </DialogDescription>
          <form method="post" className="py-4">
            <div className="grid w-full gap-1.5">
              <Label htmlFor="name" className="text-start">
                Name
              </Label>
              <Input
                type="text"
                id="name"
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </div>
          </form>
        </DialogHeader>
        <DialogFooter>
          <Button type="button" variant="outline" onClick={router.back}>
            Cancel
          </Button>
          <Button type="submit" variant="default" onClick={handleSubmit}>
            Create
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};
