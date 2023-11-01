import { CreateContactModal } from "@/features/contact/components/CreateContactModal";

export default async function Page({
  searchParams,
}: {
  searchParams: { boardRef: string; jobRef?: string };
}) {
  const { boardRef, jobRef } = searchParams;

  return <CreateContactModal boardRef={boardRef} jobRef={jobRef} />;
}
