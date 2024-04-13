import { ActivitiesNavigation } from "@/features/activity";
import { MobileActivitiesNavigation } from "@/features/activity/components/MobileActivitiesNavigation";

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
    <div className="relative flex min-h-[calc(100vh-4rem)] flex-col md:grid md:grid-cols-[250px,1fr]">
      {modal}
      <MobileActivitiesNavigation boardRef={boardReference} filter={filter} />
      <ActivitiesNavigation boardRef={boardReference} filter={filter} />
      {children}
    </div>
  );
}
