import { ActivitiesNavigation } from "@/components/activitiesNavigation";

export default async function Layout({
  children,
  modal,
  params: { boardId, filter },
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
  params: { boardId: string; filter: string };
}) {
  return (
    <div className="relative grid grid-cols-[250px,1fr]">
      {modal}
      <ActivitiesNavigation boardId={boardId} filter={filter} />
      {children}
    </div>
  );
}
