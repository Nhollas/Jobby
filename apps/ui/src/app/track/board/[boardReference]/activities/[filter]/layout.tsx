import { ActivitiesNavigation } from "@/features/activity";

export default async function Layout({
  children,
  modal,
  params: { boardReference, filter },
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
  params: { boardReference: string; filter: string };
}) {
  return (
    <div className="relative grid grid-cols-[250px,1fr]">
      {modal}
      <ActivitiesNavigation boardRef={boardReference} filter={filter} />
      {children}
    </div>
  );
}
