import { ActivitiesWrapper } from "@/features/activity";

export default async function Page({
  params: { boardReference, filter },
}: {
  params: { boardReference: string; filter: string };
}) {
  return <ActivitiesWrapper filter={filter} boardRef={boardReference} />;
}
