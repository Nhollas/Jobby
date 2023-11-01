import { Contacts } from "@/features/contact";

export default async function Page({
  params: { jobRef, boardRef },
}: {
  params: { jobRef: string; boardRef: string };
}) {
  return <Contacts boardRef={boardRef} jobRef={jobRef} url={""} />;
}
