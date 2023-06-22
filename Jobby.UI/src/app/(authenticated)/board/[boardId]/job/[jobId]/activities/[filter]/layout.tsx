import { ActivitiesNavigation } from "@/components/activitiesNavigation";

export default async function Layout({
  children,
  modal,
  params: { boardId, filter, jobId },
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
  params: { boardId: string; filter: string; jobId: string };
}) {
  return (
    <div className="relative grid h-full grid-cols-[250px,1fr]">
      {modal}
      <ActivitiesNavigation boardId={boardId} filter={filter} jobId={jobId} />
      {children}
    </div>
  );
}
