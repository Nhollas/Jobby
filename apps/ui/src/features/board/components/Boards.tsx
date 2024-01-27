"use client";

import Link from "next/link";
import { Trash2 } from "lucide-react";
import {
  Card,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
  Button,
} from "@/components/ui";
import { Board } from "@/types";

export const Boards = ({ boards }: { boards: Board[] }) => {
  return (
    <div className="w-full max-w-5xl">
      <div className="grid grid-cols-[repeat(auto-fill,minmax(250px,1fr))] gap-8">
        {boards.map((board) => (
          <Card
            key={board.reference}
            className="card"
            data-board-ref={board.reference}
          >
            <CardHeader>
              <CardTitle className="card-title truncate leading-tight">
                {board.name}
              </CardTitle>
              <CardDescription className="card-description">
                {new Date(board.createdDate).toDateString()}
              </CardDescription>
            </CardHeader>
            <CardFooter>
              <div className="flex w-full flex-row gap-x-3">
                <Button asChild size="sm" variant="outline">
                  <Link
                    href={`/track/delete-board/${board.reference}`}
                    passHref
                  >
                    <Trash2 size={18} />
                  </Link>
                </Button>
                <Button
                  asChild
                  variant="outline"
                  className="flex w-full"
                  size="sm"
                >
                  <Link href={`/track/board/${board.reference}`}>View</Link>
                </Button>
              </div>
            </CardFooter>
          </Card>
        ))}
      </div>
    </div>
  );
};
