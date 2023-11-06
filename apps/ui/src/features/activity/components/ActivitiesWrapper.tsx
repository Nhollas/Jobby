import { useBoardActivitiesQuery } from "@/features/board";
import { Activities } from "@/features/activity";

type Props = {
  boardRef: string;
  jobRef?: string;
  filter: string;
};

export function ActivitiesWrapper({ boardRef, filter, jobRef }: Props) {
  const query = useBoardActivitiesQuery(boardRef);

  if (query.isError) return <h1>Error...</h1>;
  if (query.isLoading) return <h1>Loading...</h1>;

  const activities = query.data || [];

  return (
    <Activities
      activities={activities}
      filter={filter}
      boardRef={boardRef}
      jobRef={jobRef}
    />
  );
}
