import { DeleteContactModal } from "@/features/contact/components/DeleteContactModal";

export default function Page({
  params: { contactId },
}: {
  params: { contactId: string };
}) {
  return <DeleteContactModal contactId={contactId} />;
}
