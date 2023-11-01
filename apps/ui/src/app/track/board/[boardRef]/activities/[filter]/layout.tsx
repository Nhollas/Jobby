import { ActivitiesNavigation } from "@/features/activity";

export default async function Layout({
  children,
  modal,
  params: { boardRef, filter },
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
  params: { boardRef: string; filter: string };
}) {
  return (
    <div className="relative grid grid-cols-[250px,1fr]">
      {modal}
      <ActivitiesNavigation boardRef={boardRef} filter={filter} />
      {children}
    </div>
  );
}
