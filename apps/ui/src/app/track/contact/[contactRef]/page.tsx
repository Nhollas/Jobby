import { Contact } from "@/features/contact/components";

export default async function Page({
  params: { contactRef },
}: {
  params: { contactRef: string };
}) {
  return <Contact contactRef={contactRef} />;
}
