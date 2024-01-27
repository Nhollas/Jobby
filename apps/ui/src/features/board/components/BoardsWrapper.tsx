"use client";

import { ApiErrorMessage } from "@/components";
import {
  Button,
  Card,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui";
import { Boards, useBoardsQuery } from "@/features/board";
import { boardGenerator } from "@/test/data-generators";
import { Trash2 } from "lucide-react";
import Link from "next/link";

export function BoardsWrapper() {
  const query = useBoardsQuery();

  if (query.isError)
    return (
      <ApiErrorMessage
        error={{
          status: 500,
          message: "Error...",
        }}
      />
    );

  if (query.isLoading) return <h1>Loading...</h1>;

  return <Boards boards={query.data ?? []} />;
}

function LoadingSkeleton() {
  return (
    <>
      <span className="hidden">Loading...</span>
      {Array.from({ length: 5 }, () => boardGenerator()).map((board) => (
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
                <Link href={`/track/delete-board/${board.reference}`}>
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
    </>
  );
}
