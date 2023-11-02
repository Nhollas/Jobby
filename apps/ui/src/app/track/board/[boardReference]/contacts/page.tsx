import { Contacts } from "@/features/contact";

export default function Page({
  params: { boardReference },
}: {
  params: { boardReference: string };
}) {
  return <Contacts boardRef={boardReference} />;
}
