import { DeleteContactModal } from "components/Modals/DeleteContactModal";

export default function Page({
  params: { contactId },
}: {
  params: { contactId: string };
}) {
  return <DeleteContactModal contactId={contactId} />;
}
