import { Contacts } from "@/features/contact";

export default function Page({
  params: { jobRef, boardReference },
}: {
  params: { jobRef: string; boardReference: string };
}) {
  return <Contacts boardRef={boardReference} jobRef={jobRef} />;
}
