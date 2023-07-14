import { DeleteContactModal } from "@/components/modals/DeleteContactModal";

export default function Page({
  params: { contactId },
}: {
  params: { contactId: string };
}) {
  return <DeleteContactModal contactId={contactId} />;
}
