import { DeleteContactModal } from "@/features/contact/components/DeleteContactModal";

export default function Page({
  params: { contactRef },
}: {
  params: { contactRef: string };
}) {
  return <DeleteContactModal contactRef={contactRef} />;
}
