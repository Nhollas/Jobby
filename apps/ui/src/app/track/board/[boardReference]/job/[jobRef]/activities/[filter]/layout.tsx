import { ActivitiesNavigation } from "@/features/activity";

export default async function Layout({
  children,
  modal,
  params: { boardReference, filter, jobRef },
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
  params: { boardReference: string; filter: string; jobRef: string };
}) {
  return (
    <div className="relative grid grid-cols-[250px,1fr]">
      {modal}
      <ActivitiesNavigation
        boardRef={boardReference}
        filter={filter}
        jobRef={jobRef}
      />
      {children}
    </div>
  );
}
