"use client";

import { ApiErrorMessage } from "@/components";
import { Boards, useBoardsQuery } from "@/features/board";

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
